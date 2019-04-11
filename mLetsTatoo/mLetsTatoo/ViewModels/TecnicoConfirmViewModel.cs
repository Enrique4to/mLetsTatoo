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
    public class TecnicoConfirmViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private T_tecnicos tecnico;
        private T_usuarios user;
        private bool isRunning;
        private bool isEnabled;
        private string activationCode;
        #endregion

        #region Properties
        public string ActivationCode
        {
            get { return this.activationCode; }
            set { SetValue(ref this.activationCode, value); }
        }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
        public T_tecnicos Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
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
        #endregion

        #region Contructors
        public TecnicoConfirmViewModel(T_usuarios user)
        {
            this.apiService = new ApiService();
            this.user = user;
            this.IsRunning = false;
            this.isEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand ActivateCommand
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
            //if (string.IsNullOrEmpty(this.NewPassword))
            //{
            //    this.NewPassword = user.Pass;
            //}
            ////-------------- Change Password --------------//

            //if (this.NewPassword == this.user.Pass)
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.NewPasswordError,
            //        "Ok");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.ConfirmPassword))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.ConfirmPasswordError,
            //        "Ok");
            //    return;
            //}
            //if (this.NewPassword != this.ConfirmPassword)
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.MatchPasswordError,
            //        "Ok");
            //    return;
            //}

            ////-------------- Confirm Activation --------------//

            //if (string.IsNullOrEmpty(this.ActivationCode))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.ActivationCodeError,
            //        "Ok");
            //    return;
            //}
            //var confirm = int.Parse(this.ActivationCode);
            //if (confirm != this.user.Confirmacion)
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.ActivationCodeError,
            //        "Ok");
            //    return;
            //}

            //this.IsRunning = true;
            //this.IsEnabled = false;

            //var connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;

            //    await App.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        "Ok");
            //    return;
            //}

            //var editUser = new T_usuarios
            //{
            //    Id_usuario = this.user.Id_usuario,
            //    Bloqueo = this.user.Bloqueo,
            //    Confirmacion = this.user.Confirmacion,
            //    Confirmado = true,
            //    Id_empresa = this.user.Id_empresa,
            //    Pass = this.NewPassword,
            //    Tipo = this.user.Tipo,
            //    Ucorreo = this.user.Ucorreo,
            //    Usuario = this.user.Usuario,
            //};
            //var id = this.user.Id_usuario;

            //var urlApi = App.Current.Resources["UrlAPI"].ToString();
            //var prefix = App.Current.Resources["UrlPrefix"].ToString();
            //var controller = App.Current.Resources["UrlT_usuariosController"].ToString();

            //this.apiService = new ApiService();

            //var response = await this.apiService.Put
            //    (urlApi,
            //    prefix,
            //    controller,
            //    editUser,
            //    id);

            //if (!response.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;

            //    await App.Current.MainPage.DisplayAlert(
            //    Languages.Error,
            //    response.Message,
            //    "OK");
            //    return;
            //}

            MainViewModel.GetInstance().TecnicoFeatures = new TecnicoFeaturesViewModel(user);
            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoFeaturesPage());

            this.IsRunning = false;
            this.IsEnabled = true;
        }
        #endregion
    }
}
