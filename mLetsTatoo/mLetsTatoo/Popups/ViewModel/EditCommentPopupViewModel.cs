namespace mLetsTatoo.Popups.ViewModel
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using ViewModels;
    using Xamarin.Forms;

    public class EditCommentPopupViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private T_trabajonota nota;
        private T_usuarios user;
        #endregion

        #region Properties
        public T_trabajonota Nota
        {
            get { return this.nota; }
            set { SetValue(ref this.nota, value); }
        }
        #endregion

        #region Constructors
        public EditCommentPopupViewModel(T_trabajonota nota, T_usuarios user)
        {
            this.user = user;
            this.nota = nota;
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EditCommentCommand
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
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_trabajonotaController"].ToString();

            this.nota = new T_trabajonota
            {
                Id_Nota = this.nota.Id_Nota,
                Id_Trabajo = this.nota.Id_Trabajo,
                Tipo_Usuario = this.nota.Tipo_Usuario,
                Id_De = this.nota.Id_De,
                Id_Local = this.nota.Id_Local,
                Id_Cita = this.nota.Id_Cita,
                Nota = this.nota.Nota,
                Nombre_Post = this.nota.Nombre_Post,
                Imagen_Post = this.nota.Imagen_Post,
            };
            var response = await this.apiService.Put(urlApi, prefix, controller, this.nota, this.nota.Id_Nota);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var newNota = (T_trabajonota)response.Result;

            if (this.user.Tipo == 2)
            {
                var oldNota = MainViewModel.GetInstance().TecnicoViewDate.NotaList.Where(n => n.Id_Nota == this.nota.Id_Nota).FirstOrDefault();
                if (oldNota != null)
                {
                    MainViewModel.GetInstance().TecnicoViewDate.NotaList.Remove(oldNota);
                }

                MainViewModel.GetInstance().TecnicoViewDate.NotaList.Add(newNota);
                MainViewModel.GetInstance().TecnicoViewDate.RefreshListNotas();
            }
            else if (this.user.Tipo == 1)
            {
                var oldNota = MainViewModel.GetInstance().UserViewDate.NotaList.Where(n => n.Id_Nota == this.nota.Id_Nota).FirstOrDefault();
                if (oldNota != null)
                {
                    MainViewModel.GetInstance().UserViewDate.NotaList.Remove(oldNota);
                }

                MainViewModel.GetInstance().UserViewDate.NotaList.Add(newNota);
                MainViewModel.GetInstance().UserViewDate.RefreshListNotas();
            }

            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        #endregion
    }
}
