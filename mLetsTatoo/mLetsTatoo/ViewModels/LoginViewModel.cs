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
    using mLetsTatoo.CustomPages;
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
        public T_clientes cliente;
        public T_tecnicos tecnico;
        #endregion

        #region Propierties
        public List<T_usuarios> ListUsuarios { get; set; }
        public List<T_clientes> ClienteList { get; set; }
        public List<T_tecnicos> TecnicoList { get; set; }

        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_clientes Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
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
            this.Usuario = null;
            this.Pass = null;
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
            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(this.Usuario))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirUsuario,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Pass))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirPasword,
                    "Ok");
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }
            
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.GetList<T_usuarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.ListUsuarios = (List<T_usuarios>)response.Result;

            if (this.ListUsuarios.Any(u => u.Usuario == this.Usuario && u.Pass == this.Pass))
            {
                this.user = this.ListUsuarios.Single(u => u.Usuario == this.Usuario && u.Pass == this.Pass);
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

            if (this.user.Bloqueo == true)
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
            if (this.user.Tipo > 2)
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
            if (this.user.Tipo == 1)
            {
                controller = Application.Current.Resources["UrlT_clientesController"].ToString();

                response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);
                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }
                this.ClienteList = (List<T_clientes>)response.Result;

                this.cliente = this.ClienteList.Single(c => c.Id_Usuario == this.user.Id_usuario);

                MainViewModel.GetInstance().UserHome = new UserHomeViewModel(this.user, this.cliente);
                Application.Current.MainPage = new SNavigationPage(new UserHomePage())
                {
                    BarBackgroundColor = Color.FromRgb(20, 20, 20),
                    BarTextColor = Color.FromRgb(200, 200, 200),
                    Title = this.user.Usuario.ToUpper(),
                };

                this.Usuario = string.Empty;
                this.Pass = string.Empty;
            }
            if (this.user.Tipo == 2)
            {
                controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

                response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        "OK");
                    return;
                }
                this.TecnicoList = (List<T_tecnicos>)response.Result;
                this.tecnico = this.TecnicoList.Single(c => c.Id_Usuario == this.user.Id_usuario);
                this.Usuario = string.Empty;

                this.Pass = string.Empty;

                if(this.user.Confirmado == true)
                {
                    MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel(this.user, this.tecnico);
                    Application.Current.MainPage = new SNavigationPage(new TecnicoHomePage())
                    {
                        BarBackgroundColor = Color.FromRgb(20, 20, 20),
                        BarTextColor = Color.FromRgb(200, 200, 200),
                    };
                }
                else
                {
                    MainViewModel.GetInstance().TecnicoConfirm = new TecnicoConfirmViewModel(this.user, this.tecnico);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoConfirmPage());
                }
            }

            this.IsRunning = false;
            this.IsEnabled = true;
        }
        private async void Registro()
        {
            MainViewModel.GetInstance().Register = new RegisterAccountViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterAccountPage());
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
