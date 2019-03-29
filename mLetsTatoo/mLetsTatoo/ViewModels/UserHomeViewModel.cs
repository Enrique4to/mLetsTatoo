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
        private ObservableCollection<EmpresaItemViewModel> empresas;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private T_clientes cliente;
        private T_usuarios user;
        private Image image;
        #endregion

        #region Properties
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
        public Image Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
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
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadEmpresas);
            }
        }
        public ICommand UserPageCommand
        {
            get
            {
                return new RelayCommand(GoToUserPage);
            }
        }
        #endregion

        #region Methods     
        private async void LoadCliente()
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
            var controller = App.Current.Resources["UrlT_clientesController"].ToString();

            var response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var listcte = (List<T_clientes>)response.Result;

            cliente = listcte.Single(u => u.Id_Usuario == this.user.Id_usuario);
            if (cliente.F_Perfil != null)
            {
                this.Image = new Image();
                this.Image.Source = ImageSource.FromStream(() => new MemoryStream(cliente.F_Perfil));
            }
            else
            {
                this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
            this.IsRefreshing = false;
        }
        private async void LoadEmpresas()
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
            var controller = App.Current.Resources["UrlT_empresasController"].ToString();

            var response = await this.apiService.GetList<T_empresas>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.EmpresaList = (List<T_empresas>)response.Result;
            this.RefreshEmpresaList();
            this.IsRefreshing = false;
        }
        private void RefreshEmpresaList()
        {
            var empresaSelected = this.EmpresaList.Select(e => new EmpresaItemViewModel
            {
                Bloqueo = e.Bloqueo,
                Id_Empresa = e.Id_Empresa,
                Logo = e.Logo,
                Nombre = e.Nombre,
            });
            this.Empresas = new ObservableCollection<EmpresaItemViewModel>(empresaSelected.OrderBy(e => e.Nombre));
            this.IsRefreshing = false;
        }
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
            this.TecnicoList = (List<T_tecnicos>)response.Result;
            this.RefreshTecnicoList();
            this.IsRefreshing = false;
        }
        public void RefreshTecnicoList()
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

            this.IsRefreshing = false;
        }
        private async void GoToUserPage()
        {

            this.IsRefreshing = false;
            MainViewModel.GetInstance().User = new UserViewModel(user, cliente);
            await Application.Current.MainPage.Navigation.PushAsync(new UserPage());
        }
        #endregion
    }
}
