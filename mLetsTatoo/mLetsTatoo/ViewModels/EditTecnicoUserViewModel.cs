namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using mLetsTatoo.Views;
    using Models;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Xamarin.Forms;

    public class EditTecnicoUserViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private INavigation Navigation;
        private TecnicosCollection tecnico;
        private T_usuarios user;
        private bool isActPass;
        private bool isActEmail;
        private bool isActPersonal;
        #endregion

        #region Properties
        public string CurrentPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
        public TecnicosCollection Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
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
        public EditTecnicoUserViewModel(TecnicosCollection tecnico, T_usuarios user)
        {
            this.apiService = new ApiService();
            this.tecnico = tecnico;
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
                if (this.user.Pass != this.CurrentPassword)
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
                if (this.NewPassword != this.ConfirmPassword)
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
                if (string.IsNullOrEmpty(this.Tecnico.Nombre))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.NameError,
                        "Ok");
                    return;
                }
                if (string.IsNullOrEmpty(this.Tecnico.Apellido1))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.LastnameError,
                        "Ok");
                    return;
                }

            }

            MainViewModel.GetInstance().ActivityIndicatorPopup = new ActivityIndicatorPopupViewModel();
            MainViewModel.GetInstance().ActivityIndicatorPopup.IsRunning = true;
            await Navigation.PushPopupAsync(new ActivityIndicatorPopupPage());

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
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
                Ucorreo = this.User.Ucorreo,
                Usuario = this.user.Usuario,
                F_Perfil= this.user.F_Perfil,
            };
            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_usuariosController"].ToString();

            this.apiService = new ApiService();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                editUser,
                this.user.Id_usuario);

            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var newUser = (T_usuarios)response.Result;

            var oldUser = MainViewModel.GetInstance().Login.ListUsuarios.Where(c => c.Id_usuario == this.user.Id_usuario).FirstOrDefault();
            if (oldUser != null)
            {
                MainViewModel.GetInstance().Login.ListUsuarios.Remove(oldUser);
            }
            MainViewModel.GetInstance().Login.ListUsuarios.Add(newUser);
            //this.user = (T_usuarios)response.Result;

            urlApi = Application.Current.Resources["UrlAPI"].ToString();
            prefix = Application.Current.Resources["UrlPrefix"].ToString();
            controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

            var tecnicoTemp = new T_tecnicos
            {
                Id_Empresa = this.tecnico.Id_Empresa,
                Apellido1 = this.tecnico.Apellido1,
                Apellido2 = this.tecnico.Apellido2,
                Apodo = this.tecnico.Apodo,
                Carrera = this.tecnico.Carrera,
                Id_Local = this.tecnico.Id_Local,
                Id_Tecnico = this.tecnico.Id_Tecnico,
                Id_Usuario = this.tecnico.Id_Usuario,
                Nombre = this.tecnico.Nombre,
            };

            this.apiService = new ApiService();

            response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                tecnicoTemp,
                tecnicoTemp.Id_Tecnico);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            tecnicoTemp = (T_tecnicos)response.Result;

            var oldTecnico = MainViewModel.GetInstance().Login.TecnicoList.Where(c => c.Id_Tecnico == this.tecnico.Id_Tecnico).FirstOrDefault();
            if (oldTecnico != null)
            {
                MainViewModel.GetInstance().Login.TecnicoList.Remove(oldTecnico);
            }

            var newTecnico = new TecnicosCollection
            {
                Id_Empresa = tecnicoTemp.Id_Empresa,
                Apellido1 = tecnicoTemp.Apellido1,
                Apellido2 = tecnicoTemp.Apellido2,
                Apodo = tecnicoTemp.Apodo,
                Carrera = tecnicoTemp.Carrera,
                Id_Local = tecnicoTemp.Id_Local,
                Id_Tecnico = tecnicoTemp.Id_Tecnico,
                Id_Usuario = tecnicoTemp.Id_Usuario,
                Nombre = tecnicoTemp.Nombre,
                F_Perfil = MainViewModel.GetInstance().Login.ListUsuarios.FirstOrDefault(u => u.Id_usuario == tecnicoTemp.Id_Usuario).F_Perfil,
            };

            MainViewModel.GetInstance().Login.TecnicoList.Add(newTecnico);

            MainViewModel.GetInstance().TecnicoProfile = new TecnicoProfileViewModel(this.user, this.tecnico);
            await Application.Current.MainPage.Navigation.PopModalAsync();

            await Application.Current.MainPage.Navigation.PopPopupAsync();

            this.IsActPass = false;
            this.IsActEmail = false;
            this.IsActPersonal = false;
        }
        #endregion

    }
}
