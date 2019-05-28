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
            T_notificaciones newNotif = null;
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
                    Cambio_Fecha = false,
                    F_nota = DateTime.Now.ToLocalTime(),
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


                var fromName = $"{this.cliente.Nombre} {this.cliente.Apellido}";
                var To = this.cliente.Id_Usuario;
                var notif = $"{Languages.TheClient} {fromName} {Languages.NotifNewComment} #{this.cita.Id_Cita}: {this.cita.Asunto}";
                this.apiService.SendNotificationAsync(notif, To, fromName);

                newNotif = new T_notificaciones
                {
                    Usuario_Envia = this.cliente.Id_Usuario,
                    Usuario_Recibe = this.tecnico.Id_Usuario,
                    Notificacion = notif,
                    Fecha = DateTime.Now.ToLocalTime(),
                    Visto = false,
                };
            }
            else if (this.user.Tipo == 2)
            {
                var nombre_Post = $"{this.tecnico.Nombre} {this.tecnico.Apellido} {"'"}{this.tecnico.Apodo}{"'"}";

                 this.nota = new T_trabajonota
                 {
                    Id_Trabajo = this.trabajo.Id_Trabajo,
                    Tipo_Usuario = 2,
                    Id_Usuario = this.tecnico.Id_Usuario,
                    Id_Local = this.tecnico.Id_Local,
                    Id_Cita = this.cita.Id_Cita,
                    Nota = this.AddNota,
                    Nombre_Post = nombre_Post,
                    Cambio_Fecha = false,
                     F_nota = DateTime.Now.ToLocalTime(),
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

                var fromName = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
                var To = this.cliente.Id_Usuario;
                var notif = $"{Languages.TheArtist} {fromName} {Languages.NotifNewComment} #{this.cita.Id_Cita}: {this.cita.Asunto}";
                this.apiService.SendNotificationAsync(notif, To, fromName);

                newNotif = new T_notificaciones
                {
                    Usuario_Envia = this.tecnico.Id_Usuario,
                    Usuario_Recibe = this.cliente.Id_Usuario,
                    Notificacion = notif,
                    Fecha = DateTime.Now.ToLocalTime(),
                    Visto = false,
                };

            }

            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            var response1 = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response1.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response1.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response1.Result;

            //TipoNotif Cita =1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
                TipoNotif = 1,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response1 = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response1.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response1.Message,
                "OK");
                return;
            }
        }
        #endregion
    }
}
