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
        public List<T_tecnicos> TecnicoList { get; set; }
        public List<T_trabajocitas> CitaList { get; set; }
        public List<T_trabajocitas> CteCitaList { get; set; }
        public List<T_teccaract> ListFeature { get; set; }
        public List<T_tecnicohorarios> ListHorariosTecnicos { get; set; }
        public List<EmpresasCollection> EmpresaUserList { get; set; }

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
            this.LoadCliente();
            this.LoadEmpresas();
            this.LoadTecnicos();
            this.LoadCitas();
            this.LoadFeatures();
            this.LoadHorariosTecnicos();

            this.IsRefreshing = false;
            this.TipoBusqueda = "All";

            this.apiService.EndActivityPopup();
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
        private void LoadCliente()
        {
            MainViewModel.GetInstance().UserPage = new UserViewModel(this.user, this.cliente);
        }
        private async void LoadEmpresas()
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
            var controller = Application.Current.Resources["UrlT_empresasController"].ToString();

            var response = await this.apiService.GetList<T_empresas>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.EmpresaList = (List<T_empresas>)response.Result;
            this.RefreshEmpresaList();
        }
        public void RefreshEmpresaList()
        {
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
        private async void LoadTecnicos()
        {

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
            var controller = App.Current.Resources["UrlT_tecnicosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.TecnicoList = (List<T_tecnicos>)response.Result;
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            this.TecnicoList = this.TecnicoList.Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();

            this.RefreshTecnicoList();
        }
        public void RefreshTecnicoList()
        {
            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
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
        private async void LoadCitas()
        {
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
            var controller = App.Current.Resources["UrlT_trabajocitasController"].ToString();

            var response = await this.apiService.GetList<T_trabajocitas>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.CitaList = (List<T_trabajocitas>)response.Result;
            this.CteCitaList = this.CitaList.Where(c => c.Id_Cliente == this.cliente.Id_Cliente).ToList();

            this.RefreshCitaList();
        }
        public void RefreshCitaList()
        {
            var cita = this.CteCitaList.Select(c => new CitasItemViewModel
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

            }).Where(c => c.Completa == false).ToList();

            this.Citas = new ObservableCollection<CitasItemViewModel>(cita.OrderByDescending(c => c.F_Inicio));
        }
        private async void LoadFeatures()
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
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.GetList<T_teccaract>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListFeature = (List<T_teccaract>)response.Result;
        }
        private async void LoadHorariosTecnicos()
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
            var controller = Application.Current.Resources["UrlT_tecnicohorariosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicohorarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListHorariosTecnicos = (List<T_tecnicohorarios>)response.Result;
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
            MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Search";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new SearchTecnicoPopupPage());
        }
        public async void GoToMessagesPage()
        {
            this.apiService.StartActivityPopup();

            MainViewModel.GetInstance().UserMessages = new UserMessagesViewModel(this.user, this.cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new UserMessagesPage());

            //this.apiService.EndActivityPopup();
        }
        #endregion
    }
}
