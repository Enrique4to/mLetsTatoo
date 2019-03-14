namespace mLetsTatoo.ViewModels
{
    using Xamarin.Forms;
    using Views;
    using System.ComponentModel;
    using System.Windows.Input;
    using Services;
    using GalaSoft.MvvmLight.Command;


    public class LoginViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private string usuario;
        private string pass;
        private bool isrunning;
        private bool isenabled;
        #endregion

        #region Propierties
        public string Usuario
        {
            get { return this.usuario; }
            set { SetValue(ref this.usuario, value); }
        }
        public string Pass
        {
            get { return this.pass; }
            set { SetValue(ref this.pass, value); }
        }

        public bool IsRunning
        {
            get { return this.isrunning; }
            set { SetValue(ref this.isrunning, value); }
        }
        public bool IsRemember
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get { return this.isenabled; }
            set { SetValue(ref this.isenabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsRemember = true;
            this.IsEnabled = true;
            this.Usuario = "Enrique";
            this.Pass = "Enrique";
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
        private async void Inicio()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "¡Debes introducir su usuario!",
                    "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Pass))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "¡Debes introducir su contraseña!",
                    "Ok");
                return;
            }

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "OK");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Usuario != "Enrique" || this.Pass != "Enrique")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "¡Tu usuario y/o contraseña son incorrectos!",
                    "Ok");
                this.Pass = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Usuario = string.Empty;
            this.Pass = string.Empty;

            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new HomePage();
        }

        #endregion

    }
}
