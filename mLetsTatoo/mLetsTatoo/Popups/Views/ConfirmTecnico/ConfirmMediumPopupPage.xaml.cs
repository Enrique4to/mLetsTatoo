

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
	public partial class ConfirmMediumPopupPage : PopupPage
	{
        #region Attributes
        private int time;
        private string stringtime;
        private MediaFile file;
        private int ECostAddedComision;
        private int EAdvanceAddedComision;
        private int MCostAddedComision;
        private int MAdvanceAddedComision;
        private int HCostAddedComision;
        private int HAdvanceAddedComision;
        #endregion

        public ConfirmMediumPopupPage()
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

            if (imageSender == this.MEImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file4 = this.file;
            }
            else if (imageSender == this.MMImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file5 = this.file;
            }
            else if (imageSender == this.MHImage)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.file6 = this.file;
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
                timeSender.Text = this.stringtime;
            }
            if (timeSender == this.METime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.METime = this.time;
            }
            else if (timeSender == this.MMTime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.MMTime = this.time;
            }
            else if (timeSender == this.MHTime)
            {
                MainViewModel.GetInstance().ConfirmTecnicoPopup.MHTime = this.time;
            }
        }
        private void AddComision(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            if (!string.IsNullOrEmpty(entry.Text))
            {
                int a = int.Parse(entry.Text);

                if (entry == this.ECost)
                {
                    if (a != ECostAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.ECostAddedComision = a + 50;
                        }
                    }
                }

                if (entry == this.EAdvance)
                {
                    if (a != EAdvanceAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.EAdvanceAddedComision = a + 50;
                            this.ECost.Text = null;
                        }
                    }
                }

                if (entry == this.MCost)
                {
                    if (a != MCostAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.MCostAddedComision = a + 50;
                        }
                    }
                }
                if (entry == this.MAdvance)
                {
                    if (a != MAdvanceAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.MAdvanceAddedComision = a + 50;
                            this.MCost.Text = null;
                        }
                    }
                }
                if (entry == this.HCost)
                {
                    if (a != HCostAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.HCostAddedComision = a + 50;
                        }
                    }
                }
                if (entry == this.HAdvance)
                {
                    if (a != HAdvanceAddedComision)
                    {
                        if (e.IsFocused == false)
                        {
                            entry.Text = (a + 50).ToString();
                            this.HAdvanceAddedComision = a + 50;
                            this.HCost = null;
                        }
                    }
                }
            }
        }

        #endregion
    }
}