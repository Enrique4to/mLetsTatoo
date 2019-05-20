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
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using Rg.Plugins.Popup.Extensions;
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

        public List<T_empresas> EmpresaList { get; set; }
        public List<T_trabajocitas> CitaList { get; set; }
        public List<T_trabajocitas> CteCitaList { get; set; }
        public List<T_teccaract> FeaturesList { get; set; }
        public List<T_trabajos> TrabajosList { get; set; }
        public List<T_usuarios> ListUsuarios { get; set; }
        public List<T_locales> LocalesList { get; set; }
        public List<T_ciudad> CiudadesList { get; set; }
        public List<T_estado> EstadosList { get; set; }
        public List<T_tecnicohorarios> ListHorariosTecnicos { get; set; }
        public List<T_publicaciones> ListPublicaciones { get; set; }
        public List<T_imgpublicacion> ListImgPublicacion { get; set; }
        public List<T_comentpublicacion> ListComentPublicacion { get; set; }
        public List<T_localhorarios> ListHorariosLocales { get; set; }

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
        public async void LoadLists()
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


            //-----------------------------Load LocalessList----------------------------//

            var controller = Application.Current.Resources["UrlT_localesController"].ToString();

            var response = await this.apiService.GetList<T_locales>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.LocalesList = (List<T_locales>)response.Result;


            //-----------------------------Load CiudadesList----------------------------//

            controller = Application.Current.Resources["UrlT_ciudadController"].ToString();

            response = await this.apiService.GetList<T_ciudad>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.CiudadesList = (List<T_ciudad>)response.Result;


            //-----------------------------Load EstadosList----------------------------//

            controller = Application.Current.Resources["UrlT_estadoController"].ToString();

            response = await this.apiService.GetList<T_estado>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.EstadosList = (List<T_estado>)response.Result;


            //-----------------------------Load EmpresasList----------------------------//

            controller = Application.Current.Resources["UrlT_empresasController"].ToString();

            response = await this.apiService.GetList<T_empresas>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.EmpresaList = (List<T_empresas>)response.Result;
            //-----------------------------Load CitasList----------------------------//

            controller = App.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.GetList<T_trabajocitas>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.CitaList = (List<T_trabajocitas>)response.Result;

            //-----------------------------Load FeaturesList----------------------------//

            controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            response = await this.apiService.GetList<T_teccaract>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.FeaturesList = (List<T_teccaract>)response.Result;


            //-----------------------------Load TrabajosList----------------------------//

            controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            response = await this.apiService.GetList<T_trabajos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.TrabajosList = (List<T_trabajos>)response.Result;


            //-----------------------------Load HorariosTecnicosList----------------------------//

            controller = Application.Current.Resources["UrlT_tecnicohorariosController"].ToString();

            response = await this.apiService.GetList<T_tecnicohorarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListHorariosTecnicos = (List<T_tecnicohorarios>)response.Result;


            //-----------------------------Load PublicacionesList----------------------------//

            controller = Application.Current.Resources["UrlT_publicacionesController"].ToString();

            response = await this.apiService.GetList<T_publicaciones>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListPublicaciones = (List<T_publicaciones>)response.Result;


            //-----------------------------Load ImgPublicacionesList----------------------------//

            controller = Application.Current.Resources["UrlT_imgpublicacionController"].ToString();

            response = await this.apiService.GetList<T_imgpublicacion>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListImgPublicacion = (List<T_imgpublicacion>)response.Result;


            //-----------------------------Load ComentPublicacionesList----------------------------//

            controller = Application.Current.Resources["UrlT_comentpublicacionController"].ToString();

            response = await this.apiService.GetList<T_comentpublicacion>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.ListComentPublicacion = (List<T_comentpublicacion>)response.Result;

            Application.Current.MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.FromRgb(20, 20, 20),
                BarTextColor = Color.FromRgb(200, 200, 200),
            };
        }
        private async void Inicio()
        {
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(this.Usuario))
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirUsuario,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Pass))
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.IntroducirPasword,
                    "Ok");
                return;
            }

            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();

            //-----------------------------Load UsuariosList----------------------------//

            var controller = Application.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.GetList<T_usuarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.ListUsuarios = (List<T_usuarios>)response.Result;


            //-----------------------------Load TecnicosList----------------------------//

            controller = App.Current.Resources["UrlT_tecnicosController"].ToString();

            response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            var tecnicoList = (List<T_tecnicos>)response.Result;

            this.TecnicoList = tecnicoList.Select(t => new TecnicosCollection
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
                F_Perfil = this.ListUsuarios.FirstOrDefault(u => u.Id_usuario == t.Id_Usuario).F_Perfil

            }).ToList();


            //-----------------------------Load ClientesList----------------------------//

            controller = Application.Current.Resources["UrlT_clientesController"].ToString();

            response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
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

            if (this.ListUsuarios.Any(u => u.Usuario == this.Usuario && u.Pass == this.Pass))
            {
                this.user = this.ListUsuarios.Single(u => u.Usuario == this.Usuario && u.Pass == this.Pass);
            }
            else
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
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
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AccountBloqued,
                    "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                return;
            }
            if (this.user.Tipo > 2)
            {
                this.IsEnabled = true;
                this.apiService.EndActivityPopup();
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
                this.cliente = this.ClienteList.Single(c => c.Id_Usuario == this.user.Id_usuario);
                MainViewModel.GetInstance().RergisterDevice();
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
                this.tecnico = this.TecnicoList.Single(c => c.Id_Usuario == this.user.Id_usuario);

                if(this.user.Confirmado == true)
                {
                    MainViewModel.GetInstance().RergisterDevice();
                    MainViewModel.GetInstance().TecnicoHome = new TecnicoHomeViewModel(this.user, this.tecnico);
                    Application.Current.MainPage = new SNavigationPage(new TecnicoHomePage())
                    {
                        BarBackgroundColor = Color.FromRgb(20, 20, 20),
                        BarTextColor = Color.FromRgb(200, 200, 200),
                    };
                }
                else
                {
                    MainViewModel.GetInstance().ConfirmTecnicoPopup = new ConfirmTecnicoPopupViewModel(this.user, this.tecnico);
                    MainViewModel.GetInstance().ConfirmTecnicoPopup.page = "Password";
                    await Application.Current.MainPage.Navigation.PushPopupAsync(new ConfirmPasswordPopupPage());
                    //MainViewModel.GetInstance().TecnicoConfirm = new TecnicoConfirmViewModel(this.user, this.tecnico);
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoConfirmPage());
                }

                this.Usuario = string.Empty;
                this.Pass = string.Empty;
            }

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
