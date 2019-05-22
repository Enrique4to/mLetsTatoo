namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Popups.ViewModel;
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class TecnicoProfileViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private INavigation Navigation;

        private string nombreCompleto;
        private string saldo_Favor;
        private string saldo_Contra;
        private string saldo_Retenido;

        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;

        private bool isRefreshing;
        private bool isRunning;

        public List<T_clientes> listClientes;

        public TecnicosCollection tecnico;
        public T_usuarios user;
        #endregion

        #region Properties
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
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }
        public string NombreCompleto
        {
            get { return this.nombreCompleto; }
            set { SetValue(ref this.nombreCompleto, value); }
        }
        public string Saldo_Favor
        {
            get { return this.saldo_Favor; }
            set { SetValue(ref this.saldo_Favor, value); }
        }
        public string Saldo_Contra
        {
            get { return this.saldo_Contra; }
            set { SetValue(ref this.saldo_Contra, value); }
        }
        public string Saldo_Retenido
        {
            get { return this.saldo_Retenido; }
            set { SetValue(ref this.saldo_Retenido, value); }
        }
        #endregion

        #region Constructors
        public TecnicoProfileViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.NombreCompleto = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";
            this.LoadUser();
        }
        #endregion

        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        public ICommand EditUserCommand
        {
            get
            {
                return new RelayCommand(GoToEditUser);
            }
        }
        public ICommand EditFeaturesCommand
        {
            get
            {
                return new RelayCommand(GoToEditFeatures);
            }
        }
        public ICommand SignOutCommand
        {
            get
            {
                return new RelayCommand(SignOut);
            }
        }
        #endregion
        #region Methods
        public void LoadUser()
        {
            if (this.user.F_Perfil != null)
            {
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.user.F_Perfil));
            }
            else
            {
                this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
            this.saldo_Favor = this.tecnico.Saldo_Favor.ToString("C2");
            this.saldo_Contra = this.tecnico.Saldo_Contra.ToString("C2");
            this.saldo_Retenido = this.tecnico.Saldo_Retenido.ToString("C2");
        }
        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.WhereTakePicture,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,

                    });
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync(
                    new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Small,
                    });

            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    this.SavePic();
                    return stream;
                });
            }
        }
        private async void SavePic()
        {
            this.IsRunning = true;
            
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            if (this.file != null)
            {
                this.ByteImage = FileHelper.ReadFully(this.file.GetStream());
            }

            var newUser = new T_usuarios
            {
                Id_usuario = this.user.Id_usuario,
                Bloqueo = this.user.Bloqueo,
                Confirmacion = this.user.Confirmacion,
                Confirmado = this.user.Confirmado,
                F_Perfil = this.ByteImage,
                Pass = this.user.Pass,
                Tipo = this.user.Tipo,
                Ucorreo = this.user.Ucorreo,
                Usuario = this.user.Usuario,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_usuariosController"].ToString();

            var response = await this.apiService.Put
                (urlApi,
                prefix,
                controller,
                newUser,
                this.user.Id_usuario);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            var NewUser = (T_usuarios)response.Result;

            var oldUser = MainViewModel.GetInstance().Login.ListUsuarios.Where(n => n.Id_usuario == this.user.Id_usuario).FirstOrDefault();
            if (oldUser != null)
            {
                MainViewModel.GetInstance().Login.ListUsuarios.Remove(oldUser);
            }

            MainViewModel.GetInstance().Login.ListUsuarios.Add(NewUser);
            this.IsRunning = false;
        }
        private async void GoToEditUser()
        {
            MainViewModel.GetInstance().EditTecnicoUser = new EditTecnicoUserViewModel(this.tecnico, this.user);

            await Application.Current.MainPage.Navigation.PushModalAsync(new EditTecnicoUserPage());
        }
        private async void GoToEditFeatures()
        {
            this.apiService.StartActivityPopup();

            MainViewModel.GetInstance().TecnicoEditFeatures = new TecnicoEditFeaturesViewModel(this.user, this.tecnico);
        }
        private void SignOut()
        {
        }
        #endregion
    }
}
