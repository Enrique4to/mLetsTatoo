namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Views;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
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
        private ObservableCollection<T_clientes> clientes;
        public List<T_clientes> listClientes;
        public T_clientes cliente;
        private T_usuarios user;
        #endregion
        #region Properties
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
        public UserViewModel(T_usuarios user)
        {
            this.user = user;
            //instance = this;
            this.apiService = new ApiService();
            this.IsRefreshing = false;
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
                        MaxWidthHeight = 400,
                        
                    });
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync(
                    new PickMediaOptions
                    {
                        PhotoSize= PhotoSize.Small,
                        MaxWidthHeight = 400,
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
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            byte[] ByetImage = null;

            if (this.file != null)
            {
                ByteImage = FileHelper.ReadFully(this.file.GetStream());
            }
            
            var editCliente = new T_clientes
            {
                Id_Cliente= cliente.Id_Cliente,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono,
                Id_Usuario = cliente.Id_Usuario,
                F_Nac = cliente.F_Nac,
                Bloqueo = cliente.Bloqueo,
                F_Perfil = ByteImage,
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

            this.LoadUser();
            this.IsRunning = false;
        }
        private async void LoadUser()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }
            
            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_clientesController"].ToString();

            var response = await this.apiService.GetList<T_clientes>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            this.listClientes = (List<T_clientes>)response.Result;
            this.cliente = this.listClientes.Single(u => u.Id_Usuario == this.User.Id_usuario);

            this.NombreCompleto = $"{this.cliente.Nombre} {this.cliente.Apellido}";

            if (cliente.F_Perfil != null)
            {
                this.ByteImage = this.cliente.F_Perfil;
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
            else
            {
                this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
        }
        private async void EditUser()
        {
            MainViewModel.GetInstance().EditUser = new EditUserViewModel(cliente, user);
            await Application.Current.MainPage.Navigation.PushAsync(new EditUserPage());
        }
        #endregion
        //#region Singleton
        //private static UserViewModel instance;
        //public static UserViewModel GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        return new UserViewModel();
        //    }
        //    return instance;
        //}
        //#endregion
    }


}
