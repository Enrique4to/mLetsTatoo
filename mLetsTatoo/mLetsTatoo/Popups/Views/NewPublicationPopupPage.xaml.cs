namespace mLetsTatoo.Popups.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Rg.Plugins.Popup.Pages;
    using Syncfusion.XForms.Buttons;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPublicationPopupPage : PopupPage
    {
        #region Attributes
        private Image image;
        private MediaFile file;
        private List<MediaFile> imgList;
        #endregion
        #region Constructors
        public NewPublicationPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
        }
        #endregion

        #region Methods
        public async void GetImage(object sender, EventArgs e)
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
                this.imgList = null;

                this.One_Image.IsVisible = false;
                this.Two_Image.IsVisible = false;
                this.Three_Image.IsVisible = false;
                this.Four_Image.IsVisible = false;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Medium,
                    });
            }
            else
            {
                this.imgList = await CrossMedia.Current.PickPhotosAsync(
                    new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                    },
                    new MultiPickerOptions
                    {
                        MaximumImagesCount = 10,
                    });
            }

            if (this.file != null)
            {
                this.One_Image.IsVisible = true;
                this.Two_Image.IsVisible = false;
                this.Three_Image.IsVisible = false;
                this.Four_Image.IsVisible = false;
                this.One_ImgOne.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }

            if (this.imgList != null)
            {

                if (this.imgList.Count == 1)
                {
                    this.One_Image.IsVisible = true;
                    this.Two_Image.IsVisible = false;
                    this.Three_Image.IsVisible = false;
                    this.Four_Image.IsVisible = false;
                    this.file = imgList.ElementAtOrDefault(0);
                        One_ImgOne.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        }); 
                }

                if (this.imgList.Count == 2)
                {
                    this.One_Image.IsVisible = false;
                    this.Two_Image.IsVisible = true;
                    this.Three_Image.IsVisible = false;
                    this.Four_Image.IsVisible = false;

                    this.file = imgList.ElementAtOrDefault(0);
                    this.Two_ImgOne.Source = ImageSource.FromStream(() =>
                    {
                        var stream = this.file.GetStream();
                        return stream;
                    });
                    this.file = imgList.ElementAtOrDefault(1);
                    this.Two_ImgTwo.Source = ImageSource.FromStream(() =>
                    {
                        var stream = this.file.GetStream();
                        return stream;
                    });
                }
            }
            if (this.imgList.Count == 3)
            {
                this.One_Image.IsVisible = false;
                this.Two_Image.IsVisible = false;
                this.Three_Image.IsVisible = true;
                this.Four_Image.IsVisible = false;

                this.file = imgList.ElementAtOrDefault(0);
                this.Three_ImgOne.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });

                this.file = imgList.ElementAtOrDefault(1);
                this.Three_ImgTwo.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });

                this.file = imgList.ElementAtOrDefault(2);
                this.Three_ImgThree.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }


            if (this.imgList.Count > 3)
            {
                this.One_Image.IsVisible = false;
                this.Two_Image.IsVisible = false;
                this.Three_Image.IsVisible = false;
                this.Four_Image.IsVisible = true;
                this.file = imgList.ElementAtOrDefault(0);
                this.Four_ImgOne.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
                this.file = imgList.ElementAtOrDefault(1);
                this.Four_ImgTwo.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
                this.file = imgList.ElementAtOrDefault(2);
                this.Four_ImgThree.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
                this.file = imgList.ElementAtOrDefault(3);
                this.Four_ImgFour.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }

        #endregion
    }
}