namespace mLetsTatoo.Popups.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using ViewModels;
    using Xamarin.Forms;

    public class AddCommentPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private string addNota;

        private T_trabajocitas cita;
        private T_usuarios user;
        private ClientesCollection cliente;
        private T_trabajos trabajo;
        private TecnicosCollection tecnico;
        private T_trabajonota nota;
        #endregion
        #region Properties

        public string AddNota
        {
            get { return this.addNota; }
            set { SetValue(ref this.addNota, value); }
        }
        #endregion
        #region Constructors
        public AddCommentPopupViewModel(T_usuarios user, ClientesCollection cliente, TecnicosCollection tecnico, T_trabajos trabajo, T_trabajocitas cita)
        {
            this.user = user;
            this.cliente = cliente;
            this.tecnico = tecnico;
            this.trabajo = trabajo;
            this.cita = cita;
            this.apiService = new ApiService();
        }
        #endregion
        #region Commands
        public ICommand AddNewCommentCommand
        {
            get
            {
                return new RelayCommand(SaveComment);
            }
        }
        #endregion
        #region Methods
        private async void SaveComment()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            if(this.user.Tipo == 1)
            {
                var nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}";

                this.nota = new T_trabajonota
                {
                    Id_Trabajo = this.trabajo.Id_Trabajo,
                    Tipo_Usuario = 1,
                    Id_Usuario = this.cliente.Id_Usuario,
                    Id_Local = this.tecnico.Id_Local,
                    Id_Cita = this.cita.Id_Cita,
                    Nota = this.AddNota,
                    Nombre_Post = nombre_Post,
                };
                var response = await this.apiService.Post(urlApi, prefix, controller, this.nota);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                var newNota = (T_trabajonota)response.Result;

                MainViewModel.GetInstance().UserViewDate.NotaList.Add(newNota);
                MainViewModel.GetInstance().UserViewDate.RefreshListNotas();
                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
            else if (this.user.Tipo == 2)
            {
                var nombre_Post = $"{this.tecnico.Nombre} {this.tecnico.Apellido1} {"'"}{this.tecnico.Apodo}{"'"}";

                 this.nota = new T_trabajonota
                 {
                    Id_Trabajo = this.trabajo.Id_Trabajo,
                    Tipo_Usuario = 2,
                    Id_Usuario = this.tecnico.Id_Usuario,
                    Id_Local = this.tecnico.Id_Local,
                    Id_Cita = this.cita.Id_Cita,
                    Nota = this.AddNota,
                    Nombre_Post = nombre_Post,
                };
                var response = await this.apiService.Post(urlApi, prefix, controller, this.nota);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                var newNota = (T_trabajonota)response.Result;

                MainViewModel.GetInstance().TecnicoViewDate.NotaList.Add(newNota);
                MainViewModel.GetInstance().TecnicoViewDate.RefreshListNotas();
                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
        }
        #endregion
    }
}
