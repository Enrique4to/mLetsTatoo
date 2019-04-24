namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class UserViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private string nombreCompleto;
        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;
        private bool isRefreshing;
        private bool isRunning;
        public List<T_clientes> listClientes;
        public T_clientes cliente;
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
        #endregion
        #region Constructors
        public UserViewModel(T_usuarios user, T_clientes cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.NombreCompleto = $"{this.cliente.Nombre} {this.cliente.Apellido}";
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
                return new RelayCommand(EditUser);
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
            this.user = (T_usuarios)response.Result;
            this.IsRunning = false;
        }
        private async void EditUser()
        {
            MainViewModel.GetInstance().EditUser = new EditUserViewModel(this.cliente, this.user);
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditUserPage());
        }
        #endregion
    }


}
