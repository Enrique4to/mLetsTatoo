namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using Models;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Xamarin.Forms;

    public class TecnicoViewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private INavigation Navigation;
        
        private bool isRefreshing;
        private bool isVisible;
        private bool isVisibleNewDate;
        private bool isVisibleFinishArt;
        private bool isButtonEnabled;

        private string address;
        private string studio;
        private string reference;
        private string subTotal;
        private string advance;
        private string total;
        public string hourStart;
        public string dateStart;

        private DateTime Date;
        
        public T_trabajocitas cita;
        public T_usuarios user;
        private ClientesCollection cliente;
        private T_trabajos trabajo;
        private TecnicosCollection tecnico;
        private T_locales local;
        private T_empresas empresa;
        private T_estado estado;
        private T_ciudad ciudad;
        private T_postal postal;
        public TrabajoNotaCollection notaSelected;
        public T_citaimagenes image;

        private ObservableCollection<NotasItemViewModel> notas;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsVisibleNewDate
        {
            get { return this.isVisibleNewDate; }
            set { SetValue(ref this.isVisibleNewDate, value); }
        }
        public bool IsVisibleFinishArt
        {
            get { return this.isVisibleFinishArt; }
            set { SetValue(ref this.isVisibleFinishArt, value); }
        }
        public bool IsButtonEnabled
        {
            get { return this.isButtonEnabled; }
            set { SetValue(ref this.isButtonEnabled, value); }
        }

        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }
        public string Studio
        {
            get { return this.studio; }
            set { SetValue(ref this.studio, value); }
        }
        public string Reference
        {
            get { return this.reference; }
            set { SetValue(ref this.reference, value); }
        }
        public string SubTotal
        {
            get { return this.subTotal; }
            set { SetValue(ref this.subTotal, value); }
        }
        public string Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }
        public string Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
        }
        public string HourStart
        {
            get { return this.hourStart; }
            set { SetValue(ref this.hourStart, value); }
        }
        public string DateStart
        {
            get { return this.dateStart; }
            set { SetValue(ref this.dateStart, value); }
        }

        public List<T_trabajonota> NotaList { get; set; }

        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }
        public T_citaimagenes Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }

        public ObservableCollection<NotasItemViewModel> Notas
        {
            get { return this.notas; }
            set { SetValue(ref this.notas, value); }
        }
        #endregion

        #region Constructors
        public TecnicoViewDateViewModel(T_trabajocitas cita, T_usuarios user, TecnicosCollection tecnico, T_trabajos trabajo)
        {
            this.cita = cita;
            this.user = user;
            this.tecnico = tecnico;
            this.trabajo = trabajo;
            this.apiService = new ApiService();
            this.Date = new DateTime(this.cita.F_Inicio.Year, this.cita.F_Inicio.Month, this.cita.F_Inicio.Day, this.cita.H_Inicio.Hours, this.cita.H_Inicio.Minutes, this.cita.H_Inicio.Seconds);
            this.DateStart = this.Date.ToString("dd MMM yyyy");
            this.HourStart = this.Date.ToString("h:mm tt");
            Task.Run(async () => { await this.LoadInfo(); }).Wait();
            Task.Run(async () => { await this.LoadNotas(); }).Wait();
            this.IsButtonEnabled = false;
            this.IsVisible = false;
            if (this.trabajo.Trabajo_Iniciado == true && (DateTime.Today >= this.cita.F_Inicio))
            {
                this.IsVisibleFinishArt = true;
                this.IsVisibleNewDate = true;
            }
            else if (this.trabajo.Trabajo_Iniciado == false && (DateTime.Today >= this.cita.F_Inicio))
            {
                this.IsVisibleFinishArt = true;
                this.IsVisibleNewDate = false;
            }
            else if (this.trabajo.Trabajo_Iniciado == false && (this.cita.F_Inicio > DateTime.Today))
            {
                this.IsVisibleFinishArt = false;
                this.IsVisibleNewDate = false;
            }

            this.apiService.EndActivityPopup();
        }
        #endregion

        #region Commands
        public ICommand AddNewCommentCommand
        {
            get
            {
                return new RelayCommand(GoToAddCommandPopup);
            }
        }
        public ICommand EditCommentCommand
        {
            get
            {
                return new RelayCommand(GoToEditCommentPopup);
            }
        }
        public ICommand DeleteCommentCommand
        {
            get
            {
                return new RelayCommand(DeleteComent);
            }
        }
        public ICommand RefreshNotasCommand
        {
            get
            {
                return new RelayCommand(RefreshListNotas);
            }
        }
        public ICommand ChangeDateCommand
        {
            get
            {
                return new RelayCommand(ChangeDate);
            }
        }
        public ICommand AddNewDateCommand
        {
            get
            {
                return new RelayCommand(AddNewDate);
            }
        }


        public ICommand FinishJobCommand
        {
            get
            {
                return new RelayCommand(FinishJob);
            }
        }


        #endregion

        #region Methods
        private async Task LoadInfo()
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

            //------------------------Cargar Datos de Cliente ------------------------//

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_clientesController"].ToString();

            var response = await this.apiService.Get<T_clientes>(urlApi, prefix, controller, this.trabajo.Id_Cliente);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var clienteTemp = (T_clientes)response.Result;
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            this.cliente = new ClientesCollection
            {
                Id_Cliente = clienteTemp.Id_Cliente,
                Id_Usuario = clienteTemp.Id_Usuario,
                Apellido = clienteTemp.Apellido,
                Bloqueo = clienteTemp.Bloqueo,
                Correo = clienteTemp.Correo,
                F_Nac = clienteTemp.F_Nac,
                Nombre = clienteTemp.Nombre,
                Telefono = clienteTemp.Telefono,
                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == clienteTemp.Id_Usuario).F_Perfil,
            };


            //-----------------Cargar Datos Imagenes-----------------//

            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Get<T_citaimagenes>(urlApi, prefix, controller, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.image = (T_citaimagenes)response.Result;
        }
        public async Task LoadNotas()
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

            //-----------------Cargar Notas-----------------//
            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            var response = await this.apiService.GetList<T_trabajonota>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.NotaList = (List<T_trabajonota>)response.Result;

            this.RefreshListNotas();
        }
        public void RefreshListNotas()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var nota = this.NotaList.Select(c => new NotasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_Usuario = c.Id_Usuario,
                Tipo_Usuario = c.Tipo_Usuario,
                F_nota = c.F_nota,
                Id_Local = c.Id_Local,
                Id_Nota = c.Id_Nota,
                Nota = c.Nota,
                Nombre_Post = c.Nombre_Post,
                Cambio_Fecha = c.Cambio_Fecha,
                
                F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == c.Id_Usuario).F_Perfil,

            }).Where(c => c.Id_Cita == this.cita.Id_Cita).ToList();

            this.Notas = new ObservableCollection<NotasItemViewModel>(nota.OrderByDescending(c => c.F_nota));
        }
        private async void ChangeDate()
        {
            bool answer = await Application.Current.MainPage.DisplayAlert(
                Languages.ChangeDate,
                Languages.ChangeDateAlert,
                Languages.Yes,
                "No");

            if(answer)
            {
                MainViewModel.GetInstance().NewAppointmentPopup = new NewAppointmentPopupViewModel(this.cliente);
                MainViewModel.GetInstance().NewAppointmentPopup.feature = new T_teccaract
                {
                    Alto = this.trabajo.Alto,
                    Ancho = this.trabajo.Ancho,
                    Tiempo = this.trabajo.Tiempo,
                };
                MainViewModel.GetInstance().NewAppointmentPopup.changeDate = true;
                MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this.tecnico;
                MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Calendar";
                await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
            }
            else
            {
                return;
            }
        }
        public async void SendMessage()
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
            var controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            var nombre_Post = $"{this.tecnico.Nombre} {this.tecnico.Apellido} {"'"}{this.tecnico.Apodo}{"'"}";

            var nota = new T_trabajonota
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Tipo_Usuario = 2,
                Id_Usuario = this.tecnico.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Nota = Languages.ChangeDateTecMessage,
                Nombre_Post = nombre_Post,
                F_nota = DateTime.Now,
                Id_Cita = this.cita.Id_Cita,
                Cambio_Fecha = true,
            };
            var response = await this.apiService.Post(urlApi, prefix, controller, nota);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var newNota = (T_trabajonota)response.Result;

            this.NotaList.Add(newNota);
            this.RefreshListNotas();
        }
        private async void DeleteComent()
        {
            this.apiService.StartActivityPopup();
            var answer = await Application.Current.MainPage.DisplayAlert(
                Languages.Notice,
                Languages.DeleteComment,
                Languages.Yes,
                Languages.No);
            if (!answer)
            {
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_trabajonotaController"].ToString();

            var response = await this.apiService.Delete(urlApi, prefix, controller, this.notaSelected.Id_Nota);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            var deletedNota = this.NotaList.Where(n => n.Id_Nota == this.notaSelected.Id_Nota).FirstOrDefault();
            if (deletedNota != null)
            {
                this.NotaList.Remove(deletedNota);
            }
            this.RefreshListNotas();
            this.notaSelected = null;
            IsButtonEnabled = false;
            IsVisible = false;
            this.apiService.EndActivityPopup();
        }
        private async void AddNewDate()
        {
            MainViewModel.GetInstance().NewAppointmentPopup = new NewAppointmentPopupViewModel(this.cliente);
            MainViewModel.GetInstance().NewAppointmentPopup.feature = new T_teccaract
            {
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Tiempo = this.trabajo.Tiempo,
            };
            MainViewModel.GetInstance().NewAppointmentPopup.addNewDate = true;
            MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this.tecnico;
            MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Calendar";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new AppointmentCalendarPopupPage());
        }
        private async void FinishJob()
        {
            this.apiService.StartActivityPopup();
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
            var newTrabajo = new T_trabajos
            {
                Alto = this.trabajo.Alto,
                Ancho = this.trabajo.Ancho,
                Asunto = this.trabajo.Asunto,
                Cancelado = true,
                Completo = this.trabajo.Completo,
                Costo_Cita = this.trabajo.Costo_Cita,
                Id_Caract = this.trabajo.Id_Caract,
                Id_Cliente = this.trabajo.Id_Cliente,
                Id_Tatuador = this.trabajo.Id_Tatuador,
                Id_Trabajo = this.trabajo.Id_Trabajo,
                TecnicoTiempo = this.trabajo.TecnicoTiempo,
                Tiempo = this.trabajo.Tiempo,
                Total_Aprox = this.trabajo.Total_Aprox,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Put(urlApi, prefix, controller, newTrabajo, this.trabajo.Id_Trabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newAddedTrabajo = (T_trabajos)response.Result;

            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();
            var newOldCita = new T_trabajocitas
            {
                Id_Cita = this.cita.Id_Cita,
                Id_Trabajo = this.cita.Id_Trabajo,
                Id_Cliente = this.cita.Id_Cliente,
                Id_Tatuador = this.cita.Id_Tatuador,
                F_Inicio = this.cita.F_Inicio,
                H_Inicio = this.cita.H_Inicio,
                F_Fin = this.cita.F_Fin,
                H_Fin = this.cita.H_Fin,
                Asunto = this.cita.Asunto,
                Completa = true,
                ColorText = this.cita.ColorText,
                CambioFecha = false,
                TecnicoTiempo = false,
                CitaTemp = false,
            };
            response = await this.apiService.Put(urlApi, prefix, controller, newOldCita, this.cita.Id_Cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newCitaTemp = (T_trabajocitas)response.Result;

            var oldCitaTemp = MainViewModel.GetInstance().TecnicoHome.CitasList.Where(n => n.Id_Cita == this.cita.Id_Cita).FirstOrDefault();
            if (oldCitaTemp != null)
            {
                MainViewModel.GetInstance().TecnicoHome.CitasList.Remove(oldCitaTemp);
            }

            MainViewModel.GetInstance().TecnicoHome.CitasList.Add(newCitaTemp);
            MainViewModel.GetInstance().TecnicoViewJob.RerfeshCitasList();

            var oldTrabajo = MainViewModel.GetInstance().TecnicoHome.TrabajoList.Where(c => c.Id_Trabajo == this.cita.Id_Trabajo).FirstOrDefault();

            if (oldTrabajo != null)
            {
                MainViewModel.GetInstance().TecnicoHome.TrabajoList.Remove(oldTrabajo);
            }

            MainViewModel.GetInstance().TecnicoHome.TrabajoList.Add(newAddedTrabajo);

            MainViewModel.GetInstance().TecnicoHome.RefreshTrabajosList();

            var pago = MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == this.cita.Id_Trabajo);
            if (pago.Tipo_Pago != 1)
            {
                var saldoTecnico = MainViewModel.GetInstance().Login.ListBalanceTecnico.FirstOrDefault(b => b.Id_Tecnico == this.tecnico.Id_Tecnico);
                var saldo_Favor = saldoTecnico.Saldo_Favor + (newAddedTrabajo.Costo_Cita - 50);
                var saldo_Contra = saldoTecnico.Saldo_Contra;
                var saldo_Retenido = saldoTecnico.Saldo_Retenido - (newAddedTrabajo.Costo_Cita - 50);
                decimal i = 0;
                if (saldo_Contra > 0)
                {
                    if (saldo_Favor >= saldo_Contra)
                    {
                        saldo_Favor = saldo_Favor - saldo_Contra;
                        i = saldo_Contra;
                        saldo_Contra = 0;
                    }
                    else
                    {
                        saldo_Contra = saldo_Contra - saldo_Favor;
                        i = saldo_Favor;
                        saldo_Favor = 0;
                    }
                }
                if (i > 0)
                {
                    var newPagoTecnico = new T_pagostecnico
                    {
                        Id_Tecnico = this.tecnico.Id_Tecnico,
                        Id_Usuario = this.tecnico.Id_Usuario,
                        Pago = i,
                        Fecha_Pago = DateTime.Now,
                        Concepto = Languages.PagoTecnicoConcept2,
                    };
                    controller = Application.Current.Resources["UrlT_pagostecnicoController"].ToString();

                    response = await this.apiService.Post(urlApi, prefix, controller, newPagoTecnico);

                    if (!response.IsSuccess)
                    {
                        this.apiService.EndActivityPopup();

                        await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                        return;
                    }

                    newPagoTecnico = (T_pagostecnico)response.Result;
                    MainViewModel.GetInstance().Login.ListPagosTecnico.Add(newPagoTecnico);
                }

                var newSaldoTecnico = new T_balancetecnico
                {
                    Id_Balancetecnico = saldoTecnico.Id_Balancetecnico,
                    Id_Tecnico = saldoTecnico.Id_Tecnico,
                    Id_Usuario = saldoTecnico.Id_Usuario,
                    Saldo_Contra = saldo_Contra,
                    Saldo_Favor = saldo_Favor,
                    Saldo_Retenido = saldo_Retenido,
                };
                controller = Application.Current.Resources["UrlT_balancetecnicoController"].ToString();

                response = await this.apiService.Put(urlApi, prefix, controller, newSaldoTecnico, saldoTecnico.Id_Balancetecnico);

                if (!response.IsSuccess)
                {
                    this.apiService.EndActivityPopup();

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                    return;
                }

                newSaldoTecnico = (T_balancetecnico)response.Result;
                var oldSaldoTecnico = MainViewModel.GetInstance().Login.ListBalanceTecnico.Where(p => p.Id_Balancetecnico == saldoTecnico.Id_Balancetecnico).FirstOrDefault();
                if (oldSaldoTecnico != null)
                {
                    MainViewModel.GetInstance().Login.ListBalanceTecnico.Remove(oldSaldoTecnico);
                }
                MainViewModel.GetInstance().Login.ListBalanceTecnico.Add(newSaldoTecnico);
            }
            MainViewModel.GetInstance().TecnicoHome.LoadTecnico();

                this.apiService.EndActivityPopup();

            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PopModalAsync();


            //this.cliente = MainViewModel.GetInstance().Login.ClienteList.FirstOrDefault(c => c.Id_Cliente == cita.Id_Cliente);
            var fromName = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            var To = this.cliente.Id_Usuario;
            var notif = $"{Languages.TheArtist} {fromName} {Languages.NotifFinishArt} # {trabajo.Id_Trabajo}: {trabajo.Asunto}";
            this.apiService.SendNotificationAsync(notif, To, fromName);

            var newNotif = new T_notificaciones
            {
                Usuario_Envia = this.tecnico.Id_Usuario,
                Usuario_Recibe = this.cliente.Id_Usuario,
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

            var newNotifCita = new T_notif_citas
            {
                Id_Notificacion = newNotif.Id_Notificacion,
                Id_Cita = cita.Id_Cita,
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

        public void SelectedNota()
        {
            if (this.notaSelected != null)
            {
                if(this.user.Tipo == this.notaSelected.Tipo_Usuario)
                {
                    IsButtonEnabled = true;
                    IsVisible = true;
                }
                else
                {
                    IsButtonEnabled = false;
                    IsVisible = false;
                }
            }
        }

        public void GoToAddCommandPopup()
        {
            MainViewModel.GetInstance().AddCommentPopup = new AddCommentPopupViewModel(user, cliente, tecnico, trabajo, cita);
            Navigation.PushPopupAsync(new AddCommentPopupPage());
        }
        private void GoToEditCommentPopup()
        {
            MainViewModel.GetInstance().EditCommentPopup = new EditCommentPopupViewModel(notaSelected, user);
            Navigation.PushPopupAsync(new EditCommentPopupPage());
        }
        #endregion

    }
}
