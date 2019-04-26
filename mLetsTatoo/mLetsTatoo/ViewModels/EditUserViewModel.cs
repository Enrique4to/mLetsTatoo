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
        private ClientesCollection cliente;
        private T_usuarios user;
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
        public ClientesCollection Cliente
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
        public EditUserViewModel(ClientesCollection cliente, T_usuarios user)
        {
            this.apiService = new ApiService();
            this.cliente = cliente;
            this.user = user;
            this.IsRunning = false;
            this.isEnabled = true;
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
                var cliente_phone = int.Parse(this.cliente.Telefono);
                if (cliente_phone > 0)
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
                Pass = this.NewPassword,
                Tipo = this.user.Tipo,
                Ucorreo = this.cliente.Correo,
                Usuario = this.user.Usuario,
                F_Perfil= this.user.F_Perfil,
            };
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                editUser,
                this.user.Id_usuario);

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
            //this.user = (T_usuarios)response.Result;

            urlApi = App.Current.Resources["UrlAPI"].ToString();
            prefix = App.Current.Resources["UrlPrefix"].ToString();
            controller = App.Current.Resources["UrlT_clientesController"].ToString();

            this.apiService = new ApiService();

            response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                this.cliente,
                this.cliente.Id_Cliente);

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

            //this.cliente = (T_clientes)response.Result;

            MainViewModel.GetInstance().UserPage = new UserViewModel(this.user, this.cliente);
            await Application.Current.MainPage.Navigation.PopModalAsync();

            this.IsRunning = false;
            this.IsEnabled = true;

            this.IsActPass = false;
            this.IsActEmail = false;
            this.IsActPersonal = false;
        }
        #endregion

    }
}
