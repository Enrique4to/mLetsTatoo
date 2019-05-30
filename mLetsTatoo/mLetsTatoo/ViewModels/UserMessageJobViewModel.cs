

namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.Popups.ViewModel;
    using Models;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Xamarin.Forms;

    public class UserMessageJobViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string message;
        private string appCost;
        private string appAdvance;
        private string appDuration;

        private ClientesCollection cliente;
        public TecnicosCollection tecnico;
        public T_usuarios user;
        private TrabajosTempItemViewModel trabajo;
        private T_trabajonotatemp notaTemp;
        public TrabajonotaTempCollection notaSelected;

        private ObservableCollection<NotasTempItemViewModel> notas;

        private bool isVisible;
        private bool isButtonEnabled;
        public bool pageVisible;
        #endregion

        #region Properties
        public string Message
        {
            get { return this.message; }
            set { SetValue(ref this.message, value); }
        }
        public string AppCost
        {
            get { return this.appCost; }
            set { SetValue(ref this.appCost, value); }
        }
        public string AppAdvance
        {
            get { return this.appAdvance; }
            set { SetValue(ref this.appAdvance, value); }
        }
        public string AppDuration
        {
            get { return this.appDuration; }
            set { SetValue(ref this.appDuration, value); }
        }

        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }
        public List<NotasTempItemViewModel> TrabajoNotaListtemp { get; set; }

        public ObservableCollection<NotasTempItemViewModel> Notas
        {
            get { return this.notas; }
            set { SetValue(ref this.notas, value); }
        }
        public TrabajosTempItemViewModel Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsButtonEnabled
        {
            get { return this.isButtonEnabled; }
            set { SetValue(ref this.isButtonEnabled, value); }
        }
        #endregion

        #region Constructors
        public UserMessageJobViewModel(TrabajosTempItemViewModel trabajo, ClientesCollection cliente, T_usuarios user)
        {
            this.apiService = new ApiService();
            this.cliente = cliente;
            this.user = user;
            this.trabajo = trabajo;
            this.pageVisible = false;
            this.LoadListNotas();

            IsButtonEnabled = false;
            IsVisible = false;
        }
        #endregion

        #region Commands
        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(SendMessage);
            }
        }
        public ICommand NextCommand
        {
            get
            {
                return new RelayCommand(GoToNextPopupPage);
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(Cancel);
            }
        }

        #endregion

        #region Methods
        public void LoadListNotas()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;

            var nota = MainViewModel.GetInstance().Login.TrabajoNotaTempList.Select(n => new NotasTempItemViewModel
            {
                Id_Notatemp = n.Id_Notatemp,
                Id_Trabajotemp = n.Id_Trabajotemp,
                Id_Usuario = n.Id_Usuario,

                Tipo_Usuario = n.Tipo_Usuario,
                F_nota = n.F_nota,
                Id_Local = n.Id_Local,
                Nota = n.Nota,
                Propuesta = n.Propuesta,
                Nombre_Post = n.Nombre_Post,

                Nombre =
                    n.Tipo_Usuario == 1 ?
                    this.cliente.Nombre:

                    MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Nombre,

                Apellido = 
                    n.Tipo_Usuario == 1 ?
                    this.cliente.Apellido :

                    MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Usuario == n.Id_Usuario).Apellido,

                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == n.Id_Usuario).F_Perfil

            }).Where(n => n.Id_Trabajotemp == this.trabajo.Id_Trabajotemp).ToList();

            this.TrabajoNotaListtemp = nota;

            this.Notas = new ObservableCollection<NotasTempItemViewModel>(nota.OrderBy(n => n.F_nota));
        }
        public void LoadBudget()
        {
            string tempTime = null;
            switch (this.trabajo.Tiempo)
            {
                case 30:
                    tempTime = "30 mins.";
                    break;
                case 60:
                    tempTime = "1 hr.";
                    break;
                case 90:
                    tempTime = "1.5 hrs.";
                    break;
                case 120:
                    tempTime = "2 hrs.";
                    break;
                case 150:
                    tempTime = "2.5 hrs.";
                    break;
                case 180:
                    tempTime = "3 hrs.";
                    break;
            }

            this.AppCost = $"{Languages.Cost}: {this.trabajo.Total_Aprox.ToString("C2")}";
            this.AppAdvance = $"{Languages.Advance}: {this.trabajo.Costo_Cita.ToString("C2")}";
            this.AppDuration = $"{Languages.EstimatedTime} {tempTime}";
            Application.Current.MainPage.Navigation.PushPopupAsync(new BudgetDetailsPopupPage());
        }
        private async void SendMessage()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

            var nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}";

            this.notaTemp = new T_trabajonotatemp
            {
                Id_Trabajotemp = this.trabajo.Id_Trabajotemp,
                Tipo_Usuario = 1,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(u => u.Id_Tecnico == this.trabajo.Id_Tatuador).Id_Local,
                Nota = this.Message,
                Nombre_Post = nombre_Post,
            };
            var response = await this.apiService.Post(urlApi, prefix, controller, this.notaTemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var newNota = (T_trabajonotatemp)response.Result;

            MainViewModel.GetInstance().Login.TrabajoNotaTempList.Add(newNota);
            MainViewModel.GetInstance().UserMessages.RefreshTrabajosList();

            this.LoadListNotas();
            this.Message = null;

            this.tecnico = MainViewModel.GetInstance().Login.TecnicoList.FirstOrDefault(c => c.Id_Tecnico == this.trabajo.Id_Tatuador);
            var fromName = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            var To = this.tecnico.Id_Usuario;
            var notif = $"{Languages.TheClient} {fromName} {Languages.NotifMessagePersonalized}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.cliente.Id_Usuario,
                Usuario_Recibe = this.tecnico.Id_Usuario,
                Notificacion = notif,
                Fecha = DateTime.Now.ToLocalTime(),
                Visto = false,
            };
            controller = Application.Current.Resources["UrlT_notificacionesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotif);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            newNotif = (T_notificaciones)response.Result;

            //TipoNotif cita = 1
            //TipoNotif TrabajoTemp = 2
            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_TrabajoTemp = trabajo.Id_Trabajotemp,
                TipoNotif = 2,
            };

            controller = Application.Current.Resources["UrlT_notif_citasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, newNotifCita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
        }

        private async void GoToNextPopupPage()
        {
            MainViewModel.GetInstance().NewAppointmentPopup = new NewAppointmentPopupViewModel(this.cliente);
            MainViewModel.GetInstance().NewAppointmentPopup.feature = new T_teccaract
            {
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Tiempo = this.trabajo.Tiempo,
            };
            MainViewModel.GetInstance().NewAppointmentPopup.PresupuestoPage = true;
            MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this.tecnico;
            MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Metodo";
            MainViewModel.GetInstance().NewAppointmentPopup.tempAdvance = this.trabajo.Costo_Cita;
            MainViewModel.GetInstance().NewAppointmentPopup.AppCost = this.AppCost;
            MainViewModel.GetInstance().NewAppointmentPopup.AppAdvance = this.AppAdvance;
            MainViewModel.GetInstance().NewAppointmentPopup.AppDuration = this.AppDuration;
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentMetodoPopupPage());
        }
        private async void Cancel()
        {
            this.tecnico = null;
            this.notaSelected = null;
            await Application.Current.MainPage.Navigation.PopPopupAsync();
        }

        #endregion
    }
}
