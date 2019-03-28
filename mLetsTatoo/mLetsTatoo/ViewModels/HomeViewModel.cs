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

    public class HomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRefreshing;
        private ObservableCollection<T_empresas> empresas;
        private ObservableCollection<T_tecnicos> tecnicos;
        private T_usuarios user;
        private Image image;
        #endregion
        #region Properties
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
        public ObservableCollection<T_empresas> Empresas
        {
            get { return this.empresas; }
            set { SetValue(ref this.empresas, value); }
        }
        public ObservableCollection<T_tecnicos> Tecnicos
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
        public HomeViewModel(T_usuarios user)
        {
            this.user = user;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.LoadCliente();
            this.LoadEmpresas();
            this.LoadTecnicos();
        }

        #endregion
        #region Commans
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

            var single = listcte.Single(u => u.Id_Usuario == this.User.Id_usuario);
            if (single.F_Perfil != null)
            {
                //ByteImage = single.F_Perfil;
                //ImageSource = ImageSource.FromStream(() => new MemoryStream(ByteImage));
                this.Image = new Image();
                this.Image.Source = ImageSource.FromStream(() => new MemoryStream(single.F_Perfil));
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
            var list = (List<T_empresas>)response.Result;

            this.Empresas = new ObservableCollection<T_empresas>(list);
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
            var list = (List<T_tecnicos>)response.Result;

            this.Tecnicos = new ObservableCollection<T_tecnicos>(list);
            this.IsRefreshing = false;
        }
        private async void GoToUserPage()
        {
            MainViewModel.GetInstance().User = new UserViewModel(user);
            await Application.Current.MainPage.Navigation.PushAsync(new UserPage());
        }
        #endregion
    }
}
