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
    using mLetsTatoo.Models;
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
        private string nombrecompleto;
        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;
        private bool isRefreshing;
        private bool isRunning;
        #endregion
        #region Properties
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
        public byte[] ByteImage { get; set; }
        public string NombreCompleto
        {
            get { return this.nombrecompleto; }
            set { SetValue(ref this.nombrecompleto, value); }
        }
        #endregion
        #region Constructors
        public UserViewModel()
        {
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
                        PhotoSize = PhotoSize.Small
                    });
                this.SavePic();
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
                this.SavePic();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }


        }

        private async void SavePic()
        {

            this.IsRunning = true;

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FileHelper.ReadFully(this.file.GetStream());
            }

            var viewmodel = LoginViewModel.GetInstance();

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

            var listcte = (List<T_clientes>)response.Result;
            var single = listcte.Single(u => u.Id_Usuario == viewmodel.id_usuario);

            var cliente = new T_clientes
            {
                Nombre = single.Nombre,
                Apellido = single.Apellido,
                Correo = single.Correo,
                Telefono = single.Telefono,
                F_Nac = single.F_Nac,
                Bloqueo = single.Bloqueo,
                Id_Usuario = single.Id_Usuario,
                F_Perfil = imageArray,
            };
            var id = single.Id_Cliente.ToString();

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

            var viewmodel = LoginViewModel.GetInstance();

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

            var listcte = (List<T_clientes>)response.Result;
            var single = listcte.Single(u => u.Id_Usuario == viewmodel.id_usuario);
            NombreCompleto = $"{single.Nombre} {single.Apellido}";
            if (single.F_Perfil != null)
            {
                ByteImage = single.F_Perfil;
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else
            {
                ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
        }
        #endregion
    }


    }
