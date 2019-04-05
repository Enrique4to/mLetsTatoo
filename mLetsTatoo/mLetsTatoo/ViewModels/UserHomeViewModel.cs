namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media.Abstractions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class UserHomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRefreshing;
        private bool isRunning;
        private ObservableCollection<EmpresaItemViewModel> empresas;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private T_clientes cliente;
        private T_usuarios user;
        private T_tecnicos tecnico;
        private Image image;
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
        public List<T_empresas> EmpresaList { get; set; }
        public List<T_tecnicos> TecnicoList { get; set; }
        public string NomUsuario { get; set; }
        public T_clientes Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_tecnicos Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }
        public Image Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
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
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
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
        public UserHomeViewModel(T_usuarios user)
        {
            this.user = user;
            this.NomUsuario = user.Usuario;
            this.apiService = new ApiService();
            this.LoadCliente();
            this.LoadEmpresas();
            this.LoadTecnicos();
            this.IsRunning = false;
            this.IsRefreshing = false;
            this.TipoBusqueda = "All";
        }
        #endregion

        #region Commands
        public ICommand RefreshEmpresasCommand
        {
            get
            {
                return new RelayCommand(LoadEmpresas);
            }
        }
        public ICommand RefreshArtistCommand
        {
            get
            {
                return new RelayCommand(LoadTecnicos);
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
        private async void LoadCliente()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_clientesController"].ToString();

            var response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var listcte = (List<T_clientes>)response.Result;

            this.cliente = listcte.Single(u => u.Id_Usuario == this.user.Id_usuario);
            if (this.cliente.F_Perfil != null)
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserIcon.png");
                File.WriteAllBytes(fileName, this.cliente.F_Perfil);
                this.Image = new Image();
                this.ImageSource = FileImageSource.FromFile(fileName);
            }
            else
            {
                this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }

            MainViewModel.GetInstance().UserPage = new UserViewModel(user, cliente);

            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        private async void LoadEmpresas()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsRefreshing = false;
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
                this.IsRunning = false;
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.EmpresaList = (List<T_empresas>)response.Result;

            this.IsRefreshing = false;
            this.IsRunning = false;

            this.RefreshEmpresaList();
        }
        public void RefreshEmpresaList()
        {
            if(string.IsNullOrEmpty(this.filterEmpresa))
            {
                var empresaSelected = this.EmpresaList.Select(e => new EmpresaItemViewModel
                {
                    Bloqueo = e.Bloqueo,
                    Id_Empresa = e.Id_Empresa,
                    Logo = e.Logo,
                    Nombre = e.Nombre,
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
                    Logo = e.Logo,
                    Nombre = e.Nombre,
                }).Where(e => e.Nombre.ToLower().Contains(this.filterEmpresa.ToLower())).ToList();
                this.Empresas = new ObservableCollection<EmpresaItemViewModel>(
                    empresaSelected.OrderBy(e => e.Nombre));
            }

        }
        private async void LoadTecnicos()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
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
            this.IsRefreshing = false;
            this.IsRunning = false;

            this.RefreshTecnicoList();
        }
        public void RefreshTecnicoList()
        {
            if (string.IsNullOrEmpty(this.filterTecnico))
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido1 = t.Apellido1,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    F_Perfil = t.F_Perfil,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                });
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }
            else
            {
                var tecnico = this.TecnicoList.Select(t => new TecnicoItemViewModel
                {
                    Apellido1 = t.Apellido1,
                    Apellido2 = t.Apellido2,
                    Apodo = t.Apodo,
                    Carrera = t.Carrera,
                    F_Perfil = t.F_Perfil,
                    Id_Empresa = t.Id_Empresa,
                    Id_Local = t.Id_Local,
                    Id_Tecnico = t.Id_Tecnico,
                    Nombre = t.Nombre,
                }).Where(t => t.Nombre.ToLower().Contains(this.filterTecnico.ToLower()) 
                || t.Apodo.ToLower().Contains(this.filterTecnico.ToLower())
                || t.Apellido1.ToLower().Contains(this.filterTecnico.ToLower())).ToList();
                this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
            }

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
            MainViewModel.GetInstance().CitasPage = new CitasViewModel(tecnico);
            await Application.Current.MainPage.Navigation.PushModalAsync(new CitasPage());
        }
        #endregion
    }
}
