namespace mLetsTatoo.Views
{
    using Helpers;
    using ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using Xamarin.Forms;
    using Models;
    using System.IO;

    public partial class TecnicoEditFeaturesPage : ContentPage
    {
        #region Attributes
        private int time;
        private string stringtime;
        private MediaFile file;
        #endregion

        #region Constructors
        public TecnicoEditFeaturesPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {

            var imageSender = (Image)sender;
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
            byte[] ByteImage = null;
            if (this.file != null)
            {
                imageSender.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
            ByteImage = FileHelper.ReadFully(this.file.GetStream());

            if (imageSender == this.Image1)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSE.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image2)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSM.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image3)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSH.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image4)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FME.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image5)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FMM.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image6)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FMH.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image7)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBE.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image8)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBM.Imagen_Ejemplo = ByteImage;
            }
            else if (imageSender == this.Image9)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBH.Imagen_Ejemplo = ByteImage;
            }
        }
        public async void OnTapGestureRecognizerLabel(object sender, EventArgs e)
        {
            var timeSender = (Label)sender;
            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.SelectEstimatedTime,
                Languages.Cancel,
                null,
                "30 mins",
                "1 hr",
                "1 hr 30 mins",
                "2 hrs",
                "2 hrs 30 mins",
                "3 hrs");

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == "30 mins")
            {
                this.time = 30;
                this.stringtime = "30 mins";
            }
            else if (source == "1 hr")
            {
                this.time = 60;
                this.stringtime = "1 hr";
            }
            else if (source == "1 hr 30 mins")
            {
                this.time = 90;
                this.stringtime = "1 hr 30 mins";
            }
            else if (source == "2 hrs")
            {
                this.time = 120;
                this.stringtime = "2 hrs";
            }
            else if (source == "2 hrs 30 mins")
            {
                this.time = 150;
                this.stringtime = "2 hrs 30 mins";
            }
            else if (source == "3 hrs")
            {
                this.time = 180;
                this.stringtime = "3 hrs";
            }

            if (!string.IsNullOrEmpty(stringtime))
            {
                timeSender.Text = stringtime;
            }
            if (timeSender == this.Time1)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSE.Tiempo = this.time;
            }
            else if (timeSender == this.Time2)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSM.Tiempo = this.time;
            }
            else if (timeSender == this.Time3)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FSH.Tiempo = this.time;
            }
            else if (timeSender == this.Time4)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FME.Tiempo = this.time;
            }
            else if (timeSender == this.Time5)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FMM.Tiempo = this.time;
            }
            else if (timeSender == this.Time6)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FMH.Tiempo = this.time;
            }
            else if (timeSender == this.Time7)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBE.Tiempo = this.time;
            }
            else if (timeSender == this.Time8)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBM.Tiempo = this.time;
            }
            else if (timeSender == this.Time9)
            {
                MainViewModel.GetInstance().TecnicoEditFeatures.FBH.Tiempo = this.time;
            }
        }
        private void ConvertTime(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var c = (Label)sender;
            if (c.Text == "30")
            {
                c.Text = "30 mins";
            }
            else if (c.Text == "60")
            {
                c.Text = "1 hr";
            }
            else if (c.Text == "90")
            {
                c.Text = "1 hr 30 mins";
            }
            else if (c.Text == "120")
            {
                c.Text = "2 hrs";
            }
            else if (c.Text == "150")
            {
                c.Text = "2 hrs 30 mins";
            }
            else if (c.Text == "180")
            {
                c.Text = "3 hrs";
            }
        }

        #endregion

        private void ImageChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var imageSender = (Image)sender;
            byte[] ByteImage = null;
            if (imageSender == this.Image1)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FSE.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image2)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FSM.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image3)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FSH.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image4)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FME.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image5)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FMM.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image6)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FMH.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image7)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FBE.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image8)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FBM.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
            else if (imageSender == this.Image9)
            {
                ByteImage = MainViewModel.GetInstance().TecnicoEditFeatures.FBH.Imagen_Ejemplo;
                imageSender.Source = ImageSource.FromStream(() => new MemoryStream(ByteImage));
            }
        }
    }



}