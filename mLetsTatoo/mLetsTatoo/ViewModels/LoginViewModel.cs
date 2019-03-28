namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using mLetsTatoo.Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private ObservableCollection<T_usuarios> usuarios;
        private bool isRunning;
        private bool isEnabled;
        private string pass;
        private string usuario;
        public T_usuarios user;
        #endregion
        #region Propierties
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public ObservableCollection<T_usuarios> Usuarios
        {
            get { return this.usuarios; }
            set { SetValue(ref this.usuarios, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsRemember
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string Pass
        {
            get { return this.pass; }
            set { SetValue(ref this.pass, value); }
        }
        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }
        #endregion
        #region Constructors
        public LoginViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.IsRemember = true;
            this.IsEnabled = true;
            this.Usuario = "Enrique3";
            this.Pass = "1";
        }
        #endregion
        #region Commands
        public ICommand InicioCommand
        {
            get
            {
                return new RelayCommand(Inicio);
            }

        }
        public ICommand RegistroCommand
        {
            get
            {
                return new RelayCommand(Registro);
            }

        }
        #endregion
        #region Methods
        private async void Inicio()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirUsuario,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Pass))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirPasword,
                    "Ok");
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.GetList<T_usuarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var ListUsuarios = (List<T_usuarios>)response.Result;

            if (ListUsuarios.Any(u => u.Usuario == this.Usuario && u.Pass == this.Pass))
            {
                user = ListUsuarios.Single(u => u.Usuario == this.Usuario && u.Pass == this.Pass);
            }
            else
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorUsuarioyPassword,
                    "Ok");
                this.Pass = string.Empty;
                this.Usuario = string.Empty;
                return;
            }

            if (user.Bloqueo == true)
            {
                 
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AccountBloqued,
                    "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                return;
            }

            if (!user.Confirmado == true)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorUsuarioyPassword,
                    "Ok");
                this.Pass = string.Empty;
                this.Usuario = string.Empty;
                return;
            }
            if (user.Tipo > 2)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorTypeUser,
                    "Ok");
                this.Pass = string.Empty;
                this.Usuario = string.Empty;
                return;
            }
            if (user.Tipo == 1)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                this.Usuario = string.Empty;
                this.Pass = string.Empty;

                MainViewModel.GetInstance().Home = new HomeViewModel(user);
                Application.Current.MainPage = new NavigationPage(new HomePage())
                {
                    BarBackgroundColor = Color.FromRgb(20, 20, 20),
                    BarTextColor = Color.FromRgb(200, 200, 200),
                    Title = this.user.Usuario.ToUpper(),
                };
            }
            if (this.user.Tipo == 2)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                this.Usuario = string.Empty;
                this.Pass = string.Empty;

                MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel();
                Application.Current.MainPage = new NavigationPage(new TecnicoHomePage())
                {
                    BarBackgroundColor = Color.FromRgb(20, 20, 20),
                    BarTextColor = Color.FromRgb(200, 200, 200),
                };
            }

        }
        private async void Registro()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterAccountPage());
        }
        #endregion
        #region Singleton
        private static LoginViewModel instance;
        public static LoginViewModel GetInstance()
        {
            if (instance == null)
            {
                return new LoginViewModel();
            }
            return instance;
        }
        #endregion
    }
}
