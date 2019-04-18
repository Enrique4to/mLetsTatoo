namespace mLetsTatoo.Views
{
    using Helpers;
    using ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using Xamarin.Forms;
    public partial class TecnicoFeaturesPage : ContentPage
    {
        #region Attributes
        private int time;
        private string stringtime;
        private MediaFile file;
        #endregion

        #region Constructors
        public TecnicoFeaturesPage()
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

            if (this.file != null)
            {
                imageSender.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
            if(imageSender == this.Image1)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file1 = this.file;
            }
            else if (imageSender == this.Image2)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file2 = this.file;
            }
            else if (imageSender == this.Image3)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file3 = this.file;
            }
            else if (imageSender == this.Image4)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file4 = this.file;
            }
            else if (imageSender == this.Image5)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file5 = this.file;
            }
            else if (imageSender == this.Image6)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file6 = this.file;
            }
            else if (imageSender == this.Image7)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file7 = this.file;
            }
            else if (imageSender == this.Image8)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file8 = this.file;
            }
            else if (imageSender == this.Image9)
            {
                MainViewModel.GetInstance().TecnicoFeatures.file9 = this.file;
            }
        }
        public async void OnTapGestureRecognizerLabel(object sender, EventArgs e)
        {

            var timeSender = (Label)sender;
            await CrossMedia.Current.Initialize();
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
                MainViewModel.GetInstance().TecnicoFeatures.time1 = this.time;
            }
            else if (timeSender == this.Time2)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time2 = this.time;
            }
            else if (timeSender == this.Time3)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time3 = this.time;
            }
            else if (timeSender == this.Time4)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time4 = this.time;
            }
            else if (timeSender == this.Time5)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time5 = this.time;
            }
            else if (timeSender == this.Time6)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time6 = this.time;
            }
            else if (timeSender == this.Time7)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time7 = this.time;
            }
            else if (timeSender == this.Time8)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time8 = this.time;
            }
            else if (timeSender == this.Time9)
            {
                MainViewModel.GetInstance().TecnicoFeatures.time9 = this.time;
            }
        }

        #endregion
    }



}