namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using ViewModels;
    using Xamarin.Forms;
    public class EmpresaViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRunning;
        private bool isRefreshing;
        public T_clientes cliente;
        public T_usuarios user;
        private T_usuarios tecuser;
        public T_empresas empresa;
        public T_tecnicos tecnico;
        private ObservableCollection<LocalItemViewModel> locales;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private string nombreEmpresa;
        #endregion

        #region Properties
        
        public List<T_locales> EmpresaLocalesList { get; set; }
        public List<T_tecnicos> EmpresaTecnicoList { get; set; }
        public string NombreEmpresa
        {
            get { return this.nombreEmpresa; }
            set { SetValue(ref this.nombreEmpresa, value); }
        }
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
        public T_empresas Empresa
        {
            get { return this.empresa; }
            set { SetValue(ref this.empresa, value); }
        }

        public T_tecnicos Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }
        public ObservableCollection<LocalItemViewModel> Locales
        {
            get { return this.locales; }
            set { SetValue(ref this.locales, value); }
        }
        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public EmpresaViewModel(T_empresas empresa, T_usuarios user, T_clientes cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.empresa = empresa;
            this.apiService = new ApiService();
            this.LoadLocales();
            this.LoadTecnicos();
            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshLocalesCommand
        {
            get
            {
                return new RelayCommand(LoadLocales);
            }
        }
        public ICommand RefreshArtistCommand
        {
            get
            {
                return new RelayCommand(LoadTecnicos);
            }
        }
        #endregion

        #region Methods
        private async void LoadTecnicos()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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
            //var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            //userList = userList.Where(u => u.Id_empresa == this.Empresa.Id_Empresa && u.Confirmado == true && u.Bloqueo == false).ToList();
            //userList = userList.Where(u => u.Confirmado == true).ToList();
            //userList = userList.Where(u => u.Bloqueo == false).ToList();
            this.EmpresaTecnicoList = (List<T_tecnicos>)response.Result;
            //this.EmpresaTecnicoList = this.EmpresaTecnicoList.Where(t => userList.Any(u => t.Id_Usuario == u.Id_usuario)).ToList();
            this.RefreshTecnicoList();
            this.IsRefreshing = false;
        }
        public void RefreshTecnicoList()
        {

            var userList = MainViewModel.GetInstance().Login.ListUsuarios;
            var tecnico = this.EmpresaTecnicoList.Select(t => new TecnicoItemViewModel
            {
                Apellido1 = t.Apellido1,
                Apellido2 = t.Apellido2,
                Apodo = t.Apodo,
                Carrera = t.Carrera,
                Id_Empresa = t.Id_Empresa,
                Id_Local = t.Id_Local,
                Id_Tecnico = t.Id_Tecnico,
                Id_Usuario = t.Id_Usuario,
                Nombre = t.Nombre,
                
            }).Where(t => t.Id_Empresa == this.empresa.Id_Empresa && userList.Any(u => t.Id_Usuario == u.Id_usuario && u.Confirmado == true && u.Bloqueo == false)).ToList();
            this.Tecnicos = new ObservableCollection<TecnicoItemViewModel>(tecnico.OrderBy(t => t.Apodo));
        }
        private async void LoadLocales()
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
            var controller = App.Current.Resources["UrlT_localesController"].ToString();

            var response = await this.apiService.GetList<T_locales>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var empresaList = MainViewModel.GetInstance().UserHome.EmpresaList;
            this.EmpresaLocalesList = (List<T_locales>)response.Result;
            this.RefreshLocalesList();
            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        private void RefreshLocalesList()
        {
            var localSelected = this.EmpresaLocalesList.Select(l => new LocalItemViewModel
            {
                Calle = l.Calle,
                Id_Ciudad = l.Id_Ciudad,
                Id_Colonia = l.Id_Colonia,
                Id_Cpostal = l.Id_Cpostal,
                Id_Empresa = l.Id_Empresa,
                Id_Estado = l.Id_Estado,
                Id_Local = l.Id_Local,
                Nombre = l.Nombre,
                Numero = l.Numero,
                Referencia = l.Referencia,
            }).Where(l => l.Id_Empresa == this.empresa.Id_Empresa).ToList();

            this.Locales = new ObservableCollection<LocalItemViewModel>(localSelected.OrderBy(e => e.Nombre));
        }
        #endregion

    }
}
