namespace mLetsTatoo.Popups.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ExifLib;
    using FFImageLoading.Forms;
    using Helpers;
    using mLetsTatoo.ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPublicationPopupPage : PopupPage
    {
        #region Attributes
        private MediaFile file;
        public List<MediaFile> imgList;
        private CachedImage image; 

        #endregion
        public Image Image { get; set; }
        #region Constructors
        public NewPublicationPopupPage()
        {
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
                this.Two_Image_Hor.IsVisible = false;
                this.Two_Image_Ver.IsVisible = false;
                this.Two_Image_Dif.IsVisible = false;
                this.Three_Image_Hor.IsVisible = false;
                this.Three_Image_Ver.IsVisible = false;
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
                        PhotoSize = PhotoSize.Small,
                    },
                    new MultiPickerOptions
                    {
                        MaximumImagesCount = 10,
                    });
            }

            if (this.file != null)
            {
                this.One_Image.IsVisible = true;
                this.Two_Image_Hor.IsVisible = false;
                this.Two_Image_Ver.IsVisible = false;
                this.Two_Image_Dif.IsVisible = false;
                this.Three_Image_Hor.IsVisible = false;
                this.Three_Image_Ver.IsVisible = false;
                this.Four_Image.IsVisible = false;
                this.One_ImgOne.Source = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });

                MainViewModel.GetInstance().NewPublicationPopup.file = this.file;
            }

            if (this.imgList != null)
            {

                if (this.imgList.Count == 1)
                {
                    this.One_Image.IsVisible = true;
                    this.Two_Image_Hor.IsVisible = false;
                    this.Two_Image_Ver.IsVisible = false;
                    this.Two_Image_Dif.IsVisible = false;
                    this.Three_Image_Hor.IsVisible = false;
                    this.Three_Image_Ver.IsVisible = false;
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
                    this.Three_Image_Hor.IsVisible = false;
                    this.Three_Image_Ver.IsVisible = false;
                    this.Four_Image.IsVisible = false;

                    var fileInfo1 = ExifReader.ReadJpeg(imgList.ElementAtOrDefault(0).GetStream());
                    var fileInfo2 = ExifReader.ReadJpeg(imgList.ElementAtOrDefault(1).GetStream());
                    if(fileInfo1.Width > fileInfo1.Height && fileInfo2.Width > fileInfo2.Height)
                    {
                        this.Two_Image_Hor.IsVisible = true;
                        this.Two_Image_Ver.IsVisible = false;
                        this.Two_Image_Dif.IsVisible = false;
                        this.file = imgList.ElementAtOrDefault(0);
                        this.Two_ImgOne_Hor.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        this.file = imgList.ElementAtOrDefault(1);
                        this.Two_ImgTwo_Hor.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        //Two_Hor
                    }
                    else if(fileInfo1.Width < fileInfo1.Height && fileInfo2.Width < fileInfo2.Height)
                    {
                        this.Two_Image_Hor.IsVisible = false;
                        this.Two_Image_Ver.IsVisible = true;
                        this.Two_Image_Dif.IsVisible = false;
                        this.file = imgList.ElementAtOrDefault(0);
                        this.Two_ImgOne_Ver.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        this.file = imgList.ElementAtOrDefault(1);
                        this.Two_ImgTwo_Ver.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        //Two_Ver
                    }
                    else if(
                        (fileInfo1.Width < fileInfo1.Height && fileInfo2.Width > fileInfo2.Height)
                        || (fileInfo1.Width > fileInfo1.Height && fileInfo2.Width < fileInfo2.Height))
                    {
                        this.Two_Image_Hor.IsVisible = false;
                        this.Two_Image_Ver.IsVisible = false;
                        this.Two_Image_Dif.IsVisible = true;
                        this.file = imgList.ElementAtOrDefault(0);
                        this.Two_ImgOne_Dif.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        this.file = imgList.ElementAtOrDefault(1);
                        this.Two_ImgTwo_Dif.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                        //Two_Dif
                    }
                }
                if (this.imgList.Count == 3)
                {
                    this.One_Image.IsVisible = false;
                    this.Two_Image_Hor.IsVisible = false;
                    this.Two_Image_Ver.IsVisible = false;
                    this.Two_Image_Dif.IsVisible = false;
                    this.Four_Image.IsVisible = false;

                    this.file = imgList.ElementAtOrDefault(0);
                    var fileInfo = ExifReader.ReadJpeg(this.file.GetStream());

                    if (fileInfo.Width > fileInfo.Height)
                    {
                        this.Three_Image_Hor.IsVisible = true;
                        this.Three_Image_Ver.IsVisible = false;

                        this.Three_ImgOne_Hor.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });

                        this.file = imgList.ElementAtOrDefault(1);
                        this.Three_ImgTwo_Hor.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });

                        this.file = imgList.ElementAtOrDefault(2);
                        this.Three_ImgThree_Hor.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                    }
                    else
                    {
                        this.Three_Image_Hor.IsVisible = false;
                        this.Three_Image_Ver.IsVisible = true;

                        this.Three_ImgOne_Ver.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });

                        this.file = imgList.ElementAtOrDefault(1);
                        this.Three_ImgTwo_Ver.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });

                        this.file = imgList.ElementAtOrDefault(2);
                        this.Three_ImgThree_Ver.Source = ImageSource.FromStream(() =>
                        {
                            var stream = this.file.GetStream();
                            return stream;
                        });
                    }
                }
                if (this.imgList.Count > 3)
                {
                    this.One_Image.IsVisible = false;
                    this.Two_Image_Hor.IsVisible = false;
                    this.Two_Image_Ver.IsVisible = false;
                    this.Two_Image_Dif.IsVisible = false;
                    this.Three_Image_Hor.IsVisible = false;
                    this.Three_Image_Ver.IsVisible = false;
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
                MainViewModel.GetInstance().NewPublicationPopup.imgList = this.imgList;
            }
            else
            {
                this.file = null;
                this.imgList = null;

                this.One_Image.IsVisible = false;
                this.Two_Image_Hor.IsVisible = false;
                this.Two_Image_Ver.IsVisible = false;
                this.Two_Image_Dif.IsVisible = false;
                this.Three_Image_Hor.IsVisible = false;
                this.Three_Image_Ver.IsVisible = false;
                this.Four_Image.IsVisible = false;
            }
        }            

    #endregion
}
}