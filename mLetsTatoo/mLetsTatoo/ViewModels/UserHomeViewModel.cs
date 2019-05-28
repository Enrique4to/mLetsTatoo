namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.CustomPages;
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using Models;
    using Plugin.Media.Abstractions;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class UserHomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public int empresaID;

        private byte[] byteImage;
        private ImageSource imageSource;
        private Image image;

        private bool isRefreshing;
        private bool isRunning;
        
        private ObservableCollection<EmpresaItemViewModel> empresas;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private ObservableCollection<CitasItemViewModel> citas;
        private ObservableCollection<NotificacionesItemViewModel> notificaciones;
        private ObservableCollection<T_trabajos> trabajos;

        public ClientesCollection cliente;
        public T_usuarios user;
        public TecnicosCollection tecnico;
        public T_trabajos trabajo;
        public T_trabajocitas cita;

        private string file;
        public string filter;
        public string filterEmpresa;
        public string filterTecnico;
        #endregion

        #region Properties
        public string TipoBusqueda { get; set; }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;

            }
        }
        public string NomUsuario { get; set; }

        public List<T_empresas> EmpresaList { get; set; }
        public List<TecnicosCollection> TecnicoList { get; set; }
        public List<T_trabajocitas> CitaList { get; set; }
        public List<CitasItemViewModel> CteCitaList { get; set; }
        public List<T_teccaract> FeaturesList { get; set; }
        public List<T_trabajos> TrabajosList { get; set; }
        public List<T_locales> LocalesList { get; set; }
        public List<T_ciudad> CiudadesList { get; set; }
        public List<T_estado> EstadosList { get; set; }
        public List<T_tecnicohorarios> ListHorariosTecnicos { get; set; }
        public List<EmpresasCollection> EmpresaUserList { get; set; }
        public List<NotificacionesItemViewModel> NotificacionesList { get; set; }

        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_trabajos Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }
        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }

        public ObservableCollection<EmpresaItemViewModel> Empresas
        {
            get { return this.empresas; }
            set { SetValue(ref this.empresas, value); }
        }
        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public ObservableCollection<T_trabajos> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }
        public ObservableCollection<CitasItemViewModel> Citas
        {
            get { return this.citas; }
            set { SetValue(ref this.citas, value); }
        }
        public ObservableCollection<NotificacionesItemViewModel> Notificaciones
        {
            get { return this.notificaciones; }
            set { SetValue(ref this.notificaciones, value); }
        }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public Image Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }
        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public UserHomeViewModel(T_usuarios user, ClientesCollection cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.NomUsuario = user.Usuario;
            this.apiService = new ApiService();
            this.LoadLists();
            this.IsRefreshing = false;
            this.TipoBusqueda = "All";
        }

        #endregion

        #region Commands
        public ICommand RefreshEmpresasCommand
        {
            get
            {
                return new RelayCommand(RefreshEmpresaList);
            }
        }
        public ICommand RefreshArtistCommand
        {
            get
            {
                return new RelayCommand(RefreshTecnicoList);
            }
        }
        public ICommand RefreshNotificacionesCommand
        {
            get
            {
                return new RelayCommand(RefreshNotificaciones);
            }
        }
        public ICommand RefreshCitasCommand
        {
            get
            {                
                return new RelayCommand(RefreshCitaList);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Busqueda);
            }
        }
        public ICommand NewAppointmentCommand
        {
            get
            {
                return new RelayCommand(GoToCitasPage);
            }
        }
        #endregion

        #region Methods
        public void LoadCliente()
        {
            this.cliente = new ClientesCollection
            {
                Id_Usuario = this.user.Id_usuario,
                Id_Cliente = this.cliente.Id_Cliente,
                Nombre = this.cliente.Nombre,
                Apellido = this.cliente.Apellido,
                Telefono = this.cliente.Telefono,
                Correo = this.cliente.Correo,
                F_Nac = this.cliente.F_Nac,
                Bloqueo = this.cliente.Bloqueo,
                F_Perfil = this.cliente.F_Perfil,
                Saldo_Favor =
                (
                    !MainViewModel.GetInstance().Login.ListBalanceCliente.Any(b => b.Id_Cliente == this.cliente.Id_Cliente) ?
                    0 :
                    MainViewModel.GetInstance().Login.ListBalanceCliente.FirstOrDefault(b => b.Id_Cliente == this.cliente.Id_Cliente).Saldo_Favor
                ),
            };

            MainViewModel.GetInstance().UserPage = new UserViewModel(this.user, this.cliente);
        }
        private void LoadLists()
        {
            MainViewModel.GetInstance().UserPage = new UserViewModel(this.user, this.cliente);
            this.TrabajosList = MainViewModel.GetInstance().Login.TrabajosList;
            this.CitaList = MainViewModel.GetInstance().Login.CitaList;
            this.LocalesList = MainViewModel.GetInstance().Login.LocalesList;
            this.EstadosList = MainViewModel.GetInstance().Login.EstadosList;
            this.CiudadesList = MainViewModel.GetInstance().Login.CiudadesList;
            this.FeaturesList = MainViewModel.GetInstance().Login.FeaturesList;
            this.ListHorariosTecnicos = MainViewModel.GetInstance().Login.ListHorariosTecnicos;
            
            this.RefreshEmpresaList();
            this.RefreshTecnicoList();
            this.RefreshCitaList();
            this.RefreshNotificaciones();

            this.apiService.EndActivityPopup();
        }
        public void RefreshNotificaciones()
        {
            this.NotificacionesList = MainViewModel.GetInstance().Login.NotificacionesList.Select(n => new NotificacionesItemViewModel
            {
                Id_Notificacion = n.Id_Notificacion,
                Notificacion = n.Notificacion,
                Fecha = n.Fecha,
                Usuario_Envia = n.Usuario_Envia,
                Usuario_Recibe = n.Usuario_Recibe,
                Visto = n.Visto,

                TipoNotif = MainViewModel.GetInstance().Login.Notif_CitasList.FirstOrDefault(p => p.Id_Notificacion == n.Id_Notificacion).TipoNotif,
                Id_Cita = MainViewModel.GetInstance().Login.Notif_CitasList.FirstOrDefault(p => p.Id_Notificacion == n.Id_Notificacion).Id_Cita,
                Id_TrabajoTemp = MainViewModel.GetInstance().Login.Notif_CitasList.FirstOrDefault(p => p.Id_Notificacion == n.Id_Notificacion).Id_TrabajoTemp,

            }).Where(n => n.Usuario_Recibe == this.user.Id_usuario).ToList();
            this.Notificaciones = new ObservableCollection<NotificacionesItemViewModel>(NotificacionesList.OrderBy(n => n.Fecha));
        }
        public void RefreshEmpresaList()
        {
            this.EmpresaList = MainViewModel.GetInstance().Login.EmpresaList;
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            if (string.IsNullOrEmpty(this.filterEmpresa))
            {
                var empresaSelected = this.EmpresaList.Select(e => new EmpresaItemViewModel
                {
                    Bloqueo = e.Bloqueo,
                    Id_Empresa = e.Id_Empresa,
                    Nombre = e.Nombre,
                    Id_Usuario = e.Id_Usuario,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == e.Id_Usuario).F_Perfil
                });

                this.Empresas = new ObservableCollection<EmpresaItemViewModel>(
                empresaSelected.OrderBy(e => e.Nombre));
            }
            else
            {
                var empresaSelected = this.EmpresaList.Select(e => new EmpresaItemViewModel
                {
                    Bloqueo = e.Bloqueo,
                    Id_Empresa = e.Id_Empresa,
                    Nombre = e.Nombre,
                    Id_Usuario = e.Id_Usuario,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == e.Id_Usuario).F_Perfil

                }).Where(e => e.Nombre.ToLower().Contains(this.filterEmpresa.ToLower())).ToList();

                this.Empresas = new ObservableCollection<EmpresaItemViewModel>(
                empresaSelected.OrderBy(e => e.Nombre));
            }
        }
        public void RefreshTecnicoList()
        {
            this.TecnicoList = MainViewModel.GetInstance().Login.TecnicoList;
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            this.TecnicoList = this.TecnicoList.Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();
            if (string.IsNullOrEmpty(this.filterTecnico))
            {

                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido = t.Apellido,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Id_Usuario = t.Id_Usuario,
                    Nombre = t.Nombre,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

                });
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
            else
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido = t.Apellido,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                    F_Perfil = userList.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

                }).Where(t => t.Nombre.ToLower().Contains(this.filterTecnico.ToLower()) 
                || t.Apodo.ToLower().Contains(this.filterTecnico.ToLower())
                || t.Apellido.ToLower().Contains(this.filterTecnico.ToLower())).ToList();
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
        }
        public void RefreshCitaList()
        {
            this.CteCitaList = this.CitaList.Select(c => new CitasItemViewModel
            {
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                F_Inicio = c.F_Inicio,
                H_Inicio = c.H_Inicio,
                F_Fin = c.F_Fin,
                H_Fin = c.H_Fin,
                Asunto = c.Asunto,
                Completa = c.Completa,
                ColorText = c.ColorText,
                Color = Color.FromHex(c.ColorText),
                CambioFecha = c.CambioFecha,
                TecnicoTiempo = c.TecnicoTiempo,
                CitaTemp = c.CitaTemp,
                Completado = this.TrabajosList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Completo,
                Cancelado = this.TrabajosList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Cancelado,
                Trabajo_Iniciado = this.TrabajosList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Trabajo_Iniciado,
                Pagado =
                (
                    !MainViewModel.GetInstance().Login.ListPagosCliente.Any(p => p.Id_Trabajo == c.Id_Trabajo) ?
                    true :
                    MainViewModel.GetInstance().Login.ListPagosCliente.FirstOrDefault(p => p.Id_Trabajo == c.Id_Trabajo).Pagado
                ),

            }).Where(c => 
            c.Id_Cliente == this.cliente.Id_Cliente 
            && c.Completa == false && c.Cancelado == false 
            && c.TecnicoTiempo == false && c.CitaTemp == false 
            && c.Pagado == true
            ).ToList();

            this.Citas = new ObservableCollection<CitasItemViewModel>(CteCitaList.OrderByDescending(c => c.F_Inicio));
            IsRefreshing = false;
        }

        public void Busqueda()
        {
            if (TipoBusqueda == "All")
            {
            }
            if (TipoBusqueda == "Studios")
            {
                this.filterEmpresa = this.filter;
                this.RefreshEmpresaList();
            }
            if (TipoBusqueda == "Citas")
            {
            }
            if (TipoBusqueda == "Artists")
            {
                this.filterTecnico = this.filter;
                this.RefreshTecnicoList();
            }
        }
        private async void GoToCitasPage()
        {
            MainViewModel.GetInstance().NewAppointmentPopup= new NewAppointmentPopupViewModel(this.cliente);
            MainViewModel.GetInstance().NewAppointmentPopup.fromTecnitoPage = false;
            MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Search";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new SearchTecnicoPopupPage());
        }
        public async void GoToMessagesPage()
        {
            MainViewModel.GetInstance().UserMessages = new UserMessagesViewModel(this.user, this.cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new UserMessagesPage());
        }

        #endregion
    }
}
