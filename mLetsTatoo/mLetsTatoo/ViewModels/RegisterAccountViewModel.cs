namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class RegisterAccountViewModel : BaseViewModel 
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private byte[] byteImage;
        private bool isRunning;
        private bool isEnabled;
        private int id_usuario;

        private T_usuarios user;
        private T_clientes cliente;
        #endregion

        #region Properties
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
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
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        #endregion

        #region Constructors
        public RegisterAccountViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand ActivateCommand
        {
            get
            {
                return new RelayCommand(Activate);
            }
        }
        #endregion

        #region Methods
        private async void Activate()
        {
            //--------------------- Validación del usuario ---------------------//

            this.IsRunning = true;
            this.IsEnabled = false;

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

            if (string.IsNullOrEmpty(this.User))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.UserError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.ConfirmPassword))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmPasswordError,
                    "Ok");
                return;
            }
            if (this.Password != this.ConfirmPassword)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.MatchPasswordError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailError,
                    "Ok");
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
            var list = (List<T_usuarios>)response.Result;

            if (list.Any(u => u.Usuario == this.User))
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.UserExistError,
                    "Ok");
                this.User = string.Empty;
                return;
            }

            if (list.Any(u => u.Ucorreo == this.Email))
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailExistError,
                    "Ok");
                this.User = string.Empty;
                return;
            }

            //--------------------- Validación datos del Cliente ---------------------//

            if (string.IsNullOrEmpty(this.Name))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Lastname))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastnameError,
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Phone))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneError,
                    "Ok");
                return;
            }

            var bloqueo = false;
            var clibloq = false;
            DateTime now = DateTime.Today;
            int age = now.Year - Birthdate.Year;
            if (age < 18)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                clibloq = true;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.AgeError,
                "Ok");
                return;
            }

            controller = App.Current.Resources["UrlT_clientesController"].ToString();
            response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                return;
            }

            var listcte = (List<T_clientes>)response.Result;

            if (listcte.Any(u => u.Correo == this.Email && u.Bloqueo == true))
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

            var rnd = new Random();
            var Activacion = rnd.Next(100000, 999999);
            this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
            var usuario = new T_usuarios
            {
                Usuario = this.User,
                Pass = this.Password,
                Confirmacion = Activacion,
                Bloqueo = bloqueo,
                Confirmado = true,
                Tipo = 1,
                Ucorreo = this.Email,
                F_Perfil = this.ByteImage,
            };

            controller = App.Current.Resources["UrlT_usuariosController"].ToString();
            response = await this.apiService.Post(urlApi, prefix, controller, usuario);

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
            this.user = (T_usuarios)response.Result;
            if (this.Phone.Length > 10)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await App.Current.MainPage.DisplayAlert(
                Languages.PhoneLenghtError,
                response.Message,
                "OK");
                return;
            }
            var cliente = new T_clientes
            {
                Nombre = this.Name,
                Apellido = this.Lastname,
                Correo = this.Email,
                Telefono = Phone,
                Id_Usuario = this.user.Id_usuario,
                F_Nac = this.Birthdate,
                Bloqueo = clibloq,
            };
            cliente = (T_clientes)response.Result;

            controller = App.Current.Resources["UrlT_clientesController"].ToString();
            response = await this.apiService.Post(urlApi, prefix, controller, cliente);

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

            var balancecliente = new T_balancecliente
            {
                Id_Usuario = this.user.Id_usuario,
                Id_Cliente = cliente.Id_Cliente,
                Saldo_Favor = 0,
            };

            controller = App.Current.Resources["UrlT_balanceclienteController"].ToString();
            response = await this.apiService.Post(urlApi, prefix, controller, cliente);

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

            await Application.Current.MainPage.Navigation.PopModalAsync();

            this.IsRunning = false;
            this.IsEnabled = true;
        }
        #endregion
    }
}
