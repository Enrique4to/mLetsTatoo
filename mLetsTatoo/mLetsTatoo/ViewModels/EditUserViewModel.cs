
namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Views;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class EditUserViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private T_clientes cliente;
        private T_usuarios user;
        private bool isRefreshing;
        private bool isRunning;
        private bool isEnabled;
        private bool isActPass;
        private bool isActEmail;
        private bool isActPersonal;
        #endregion

        #region Properties

        public string CurrentPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
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
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsActPass
        {
            get { return this.isActPass; }
            set { SetValue(ref this.isActPass, value); }
        }
        public bool IsActEmail
        {
            get { return this.isActEmail; }
            set { SetValue(ref this.isActEmail, value); }
        }
        public bool IsActPersonal
        {
            get { return this.isActPersonal; }
            set { SetValue(ref this.isActPersonal, value); }
        }
        #endregion

        #region Contructors
        public EditUserViewModel(T_clientes cliente, T_usuarios user)
        {
            this.apiService = new ApiService();
            this.cliente = cliente;
            this.user = user;
        }
        #endregion

        #region Commands
        public ICommand SaveUserDataCommand
        {
            get
            {
                return new RelayCommand(SaveUserData);
            }
        }

        #endregion

        #region Methods
        private async void SaveUserData()
        {
            if (string.IsNullOrEmpty(this.NewPassword))
            {
                this.NewPassword = user.Pass;
            }
            //-------------- Change Password --------------//
            if (this.IsActPass==true)
            {
                if (string.IsNullOrEmpty(this.CurrentPassword))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.NoCurrentPassword,
                        "Ok");
                    return;
                }
                if (this.user.Pass == this.CurrentPassword)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.CurrentPasswordError,
                        "Ok");
                    return;
                }
                if (this.NewPassword == this.CurrentPassword)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.NewPasswordError,
                        "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(this.ConfirmPassword))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.ConfirmPasswordError,
                        "Ok");
                    return;
                }
                if (this.NewPassword!=this.ConfirmPassword)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.MatchPasswordError,
                        "Ok");
                    return;
                }

            }

            //-------------- Change Email --------------//
            if (this.IsActEmail == true)
            {
                if (string.IsNullOrEmpty(this.user.Ucorreo))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.EmailError,
                        "Ok");
                    return;
                }
            }

            //-------------- Change Personal --------------//
            if (this.IsActEmail == true)
            {
                if (string.IsNullOrEmpty(this.cliente.Nombre))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.NameError,
                        "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(this.cliente.Apellido))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.LastnameError,
                        "Ok");
                    return;
                }
                if (this.cliente.Telefono > 0)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PhoneError,
                        "Ok");
                    return;
                }
                DateTime now = DateTime.Today;
                int age = now.Year - this.cliente.F_Nac.Year;
                if (age < 18)
                {
                    await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AgeError,
                    "Ok");
                    return;
                }

            }
            this.IsRunning = true;
            this.IsEnabled = false;


            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var editUser = new T_usuarios
            {
                Id_usuario = this.user.Id_usuario,
                Bloqueo = this.user.Bloqueo,
                Confirmacion = this.user.Confirmacion,
                Confirmado = this.user.Confirmado,
                Id_empresa = this.user.Id_empresa,
                Pass = this.NewPassword,
                Tipo = this.user.Tipo,
                Ucorreo = this.cliente.Correo,
                Usuario = this.user.Usuario,
            };
            var id = this.user.Id_usuario;
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                editUser,
                id);

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
            this.IsRunning = false;


            id = cliente.Id_Cliente;
            urlApi = App.Current.Resources["UrlAPI"].ToString();
            prefix = App.Current.Resources["UrlPrefix"].ToString();
            controller = App.Current.Resources["UrlT_clientesController"].ToString();

            this.apiService = new ApiService();

            response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                cliente,
                id);

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

            this.IsRunning = false;
            this.IsEnabled = true;
            this.IsActPass = false;
            this.IsActEmail = false;
            this.IsActPersonal = false;

            MainViewModel.GetInstance().UserPage = new UserViewModel(user, cliente);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

    }
}
