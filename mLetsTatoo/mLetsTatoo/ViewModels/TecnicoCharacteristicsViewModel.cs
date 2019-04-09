

namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using mLetsTatoo.Helpers;
    using Models;
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;

    public class TecnicoFeaturesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
        #region Attributes
        private T_usuarios user;
        private T_tecnicos tecnico;
        private bool isRunning;

        private byte[] byteImage;
        private ImageSource imageSource1;
        private ImageSource imageSource2;
        private ImageSource imageSource3;
        private ImageSource imageSource4;
        private ImageSource imageSource5;
        private ImageSource imageSource6;
        private ImageSource imageSource7;
        private ImageSource imageSource8;
        private ImageSource imageSource9;
        private MediaFile file;

        #endregion
        #region Properties

        public bool IsRunning
        { 
                get { return this.isRunning; }
                set { SetValue(ref this.isRunning, value); }
        }

        public ImageSource ImageSource1
        {
            get { return this.imageSource1; }
            set { SetValue(ref this.imageSource1, value); }
        }
        public ImageSource ImageSource2
        {
            get { return this.imageSource2; }
            set { SetValue(ref this.imageSource2, value); }
        }
        public ImageSource ImageSource3
        {
            get { return this.imageSource3; }
            set { SetValue(ref this.imageSource3, value); }
        }
        public ImageSource ImageSource4
        {
            get { return this.imageSource4; }
            set { SetValue(ref this.imageSource4, value); }
        }
        public ImageSource ImageSource5
        {
            get { return this.imageSource5; }
            set { SetValue(ref this.imageSource5, value); }
        }
        public ImageSource ImageSource6
        {
            get { return this.imageSource6; }
            set { SetValue(ref this.imageSource6, value); }
        }
        public ImageSource ImageSource7
        {
            get { return this.imageSource7; }
            set { SetValue(ref this.imageSource7, value); }
        }
        public ImageSource ImageSource8
        {
            get { return this.imageSource8; }
            set { SetValue(ref this.imageSource8, value); }
        }
        public ImageSource ImageSource9
        {
            get { return this.imageSource9; }
            set { SetValue(ref this.imageSource9, value); }
        }
        #endregion
        #region Constructors
        public TecnicoFeaturesViewModel(T_usuarios user)
        {

            Application.Current.MainPage.DisplayAlert(
                Languages.Notice,
                Languages.CompleteFeatures,
                "Ok");

            this.user = user;
            this.apiService = new ApiService();
            this.LoadFeatures();
            this.LoadTecnico();
        }

        private void LoadFeatures()
        {
            this.ImageSource1 = "Image.png";
            this.ImageSource2 = "Image.png";
            this.ImageSource3 = "Image.png";
            this.ImageSource4 = "Image.png";
            this.ImageSource5 = "Image.png";
            this.ImageSource6 = "Image.png";
            this.ImageSource7 = "Image.png";
            this.ImageSource8 = "Image.png";
            this.ImageSource9 = "Image.png";

            //if (this.cliente.F_Perfil != null)
            //{
            //    this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.cliente.F_Perfil));
            //}
            //else
            //{
            //    this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
            //    this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            //}
        }
        #endregion
        #region Commands

        #endregion
        #region Merhods
        private async void LoadTecnico()
        {
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }

            var listcte = (List<T_tecnicos>)response.Result;

            this.tecnico = listcte.Single(t => t.Id_Usuario == this.user.Id_usuario);

            this.IsRunning = false;
        }
        #endregion
    }
}
