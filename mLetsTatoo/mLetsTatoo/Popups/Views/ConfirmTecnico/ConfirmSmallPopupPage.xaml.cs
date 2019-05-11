

namespace mLetsTatoo.Popups.Views
{
    using mLetsTatoo.Helpers;
    using mLetsTatoo.ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Rg.Plugins.Popup.Pages;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmSmallPopupPage : PopupPage
	{
        #region Attributes
        private int time;
        private string stringtime;
        private MediaFile file;
        #endregion

        public ConfirmSmallPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
		}

        #region Methods
        public async void GetImage(object sender, EventArgs e)
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

            if (imageSender == this.SEImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file1 = this.file;
            }
            else if (imageSender == this.SMImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file2 = this.file;
            }
            else if (imageSender == this.SHImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file3 = this.file;
            }
        }
        public async void GetTime(object sender, EventArgs e)
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
                timeSender.Text = Languages.EstimatedTime;
            }
            if (timeSender == this.SETime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.SETime = this.time;
            }
            else if (timeSender == this.SMTime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.SMTime = this.time;
            }
            else if (timeSender == this.SHTime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.SHTime = this.time;
            }
        }

        #endregion
    }
}