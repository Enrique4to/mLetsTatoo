
namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using Models;
    using Services;

    public class EditUserViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private T_clientes cliente;
        private T_usuarios user;
        private bool isRefreshing;
        private bool isRunning;
        private bool isEnabled;
        private string currentPassword;
        private string newPassword;
        private string confirmPassword;
        private string email;
        #endregion
        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string CurrentPassword
        {
            get { return this.currentPassword; }
            set { SetValue(ref this.currentPassword, value); }
        }
        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set { SetValue(ref this.confirmPassword, value); }
        }
        public string NewPassword
        {
            get { return this.newPassword; }
            set { SetValue(ref this.newPassword, value); }
        }
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
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }
            if(string.IsNullOrEmpty(this.currentPassword))
            {
                newPassword = user.Pass;
            }
            if (string.IsNullOrEmpty(this.email))
            {
                email = user.Ucorreo;
            }
            var editUsiario = new T_usuarios
            {
                Id_usuario = user.Id_usuario,
                Bloqueo = user.Bloqueo,
                Confirmacion = user.Confirmacion,
                Confirmado = user.Confirmado,
                Id_empresa = user.Id_empresa,
                Pass = NewPassword,
                Tipo = user.Tipo,
                Ucorreo = Email,
                Usuario = user.Usuario,
            };

            var editCliente = new T_clientes
            {
                Id_Cliente = cliente.Id_Cliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono,
                Id_Usuario = cliente.Id_Usuario,
                F_Nac = cliente.F_Nac,
                Bloqueo = cliente.Bloqueo,
                F_Perfil = cliente.F_Perfil
            };
            var id = cliente.Id_Cliente;
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_clientesController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                editCliente,
                id);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.IsRunning = false;
        }

        #endregion

    }
}
