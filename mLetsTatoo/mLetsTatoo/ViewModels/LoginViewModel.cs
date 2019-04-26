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

        private bool isRunning;
        private bool isEnabled;
        private string pass;
        private string usuario;

        public T_usuarios user;
        public ClientesCollection cliente;
        public TecnicosCollection tecnico;
        #endregion

        #region Propierties
        public List<T_usuarios> ListUsuarios { get; set; }
        public List<ClientesCollection> ClienteList { get; set; }
        public List<TecnicosCollection> TecnicoList { get; set; }

        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
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
            
            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.GetList<T_usuarios>(urlApi, prefix, controller);
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
                var clienteList = (List<T_clientes>)response.Result;

                this.ClienteList = clienteList.Select(c => new ClientesCollection
                {
                    Id_Cliente = c.Id_Cliente,
                    Id_Usuario = c.Id_Usuario,
                    Apellido = c.Apellido,
                    Bloqueo = c.Bloqueo,
                    Correo = c.Correo,
                    F_Nac = c.F_Nac,
                    Nombre = c.Nombre,
                    Telefono = c.Telefono,
                    F_Perfil = this.ListUsuarios.FirstOrDefault(u => u.Id_usuario == c.Id_Usuario).F_Perfil,

                }).ToList();

                this.cliente = this.ClienteList.Single(c => c.Id_Usuario == this.user.Id_usuario);

                MainViewModel.GetInstance().UserHome = new UserHomeViewModel(this.user, this.cliente);
                Application.Current.MainPage = new SNavigationPage(new UserHomePage())
                {
                    BarBackgroundColor = Color.FromRgb(20, 20, 20),
                    BarTextColor = Color.FromRgb(200, 200, 200),
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
                var tecnicoList = (List<T_tecnicos>)response.Result;

                this.TecnicoList = tecnicoList.Select(t => new TecnicosCollection
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
                    F_Perfil = this.ListUsuarios.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

                }).ToList();
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

    }
}
