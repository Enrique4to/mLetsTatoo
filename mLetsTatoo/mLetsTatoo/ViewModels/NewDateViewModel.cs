namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Models;
    using mLetsTatoo.Services;
    using mLetsTatoo.Views;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    public class NewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;
        private bool isVisivle;
        public string filter;
        private string selectedArtist;
        private string describeArt;
        public T_tecnicos tecnico;
        public T_usuarios user;
        public T_clientes cliente;
        public decimal cost;
        public decimal advance;
        private byte[] byteImage;
        private ImageSource imageSource;
        private MediaFile file;

        private bool smallChecked;
        private bool mediumSizeChecked;
        private bool bigChecked;
        private bool easyChecked;
        private bool mediumComplexityChecked;
        private bool hardChecked;
        #endregion

        #region Properties
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool SmallChecked
        {
            get { return this.smallChecked; }
            set { SetValue(ref this.smallChecked, value); }
        }
        public bool MediumSizeChecked
        {
            get { return this.mediumSizeChecked; }
            set { SetValue(ref this.mediumSizeChecked, value); }
        }
        public bool BigChecked
        {
            get { return this.bigChecked; }
            set { SetValue(ref this.bigChecked, value); }
        }
        public bool EasyChecked
        {
            get { return this.easyChecked; }
            set { SetValue(ref this.easyChecked, value); }
        }
        public bool MediumComplexityChecked
        {
            get { return this.mediumComplexityChecked; }
            set { SetValue(ref this.mediumComplexityChecked, value); }
        }
        public bool HardChecked
        {
            get { return this.hardChecked; }
            set { SetValue(ref this.hardChecked, value); }
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
        public bool IsVisible
        {
            get { return this.isVisivle; }
            set { SetValue(ref this.isVisivle, value); }
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
        public string Filter
        {
            get { return this.filter; }
            set { SetValue(ref this.filter, value); }
        }
        public string SelectedArtist
        {
            get { return this.selectedArtist; }
            set { SetValue(ref this.selectedArtist, value); }
        }
        public string DescribeArt
        {
            get { return this.describeArt; }
            set { SetValue(ref this.describeArt, value); }
        }
        public decimal Cost
        {
            get { return this.cost; }
            set { SetValue(ref this.cost, value); }
        }
        public decimal Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }
        #endregion

        #region Constructors
        public NewDateViewModel(T_tecnicos tecnico, T_usuarios user, T_clientes cliente)
        {
            this.tecnico = tecnico;
            this.user = user;
            this.cliente = cliente;

            this.apiService = new ApiService();

            this.smallChecked = true;
            this.easyChecked = true;


            if (MainViewModel.GetInstance().Tecnico == null)
            {
                this.selectedArtist = Languages.SelectArtist;
            }
            else
            {
                this.selectedArtist = MainViewModel.GetInstance().Tecnico.SelectedArtist;
            }
            if(this.tecnico != null)
            {
                this.selectedArtist = $"Artista: {this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido1}";
            }

            this.LoadFeatures();
        }
        #endregion

        #region Commands
        public ICommand SearchArtistCommand
        {
            get
            {
                return new RelayCommand(GoToSearch);
            }
        }
        public ICommand SaveDateCommand
        {
            get
            {
                return new RelayCommand(SaveDate);
            }
        }
        public ICommand AddArtImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        #endregion

        #region Methods
        private async void GoToSearch()
        {
            MainViewModel.GetInstance().Search = new SearchViewModel();
            await Application.Current.MainPage.Navigation.PushModalAsync(new SearchPage());
        }
        private async void SaveDate()
        {
            if(string.IsNullOrEmpty(this.selectedArtist))
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.SelectedArtistError,
                "Ok");
                return;
            }
            if (this.AppointmentDate < DateTime.Today)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.DateError,
                "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.describeArt))
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.DescribeArtError,
                "Ok");
                return;
            }
        }
        public void LoadFeatures()
        {
            if (smallChecked == true)
            {
                if (easyChecked == true)
                {
                    this.cost = 300;
                    this.advance = 150;
                }
                else if (mediumComplexityChecked == true)
                {
                    this.cost = 500;
                    this.advance = 150;
                }
                else if (HardChecked == true)
                {
                    this.cost = 800;
                    this.advance = 150;
                }
            }
            if (mediumSizeChecked == true)
            {
                if (easyChecked == true)
                {
                    this.cost = 800;
                    this.advance = 150;
                }
                else if (mediumComplexityChecked == true)
                {
                    this.cost = 1200;
                    this.advance = 150;
                }
                else if (HardChecked == true)
                {
                    this.cost = 1500;
                    this.advance = 150;
                }
            }
            if (bigChecked == true)
            {
                if (easyChecked == true)
                {
                    this.cost = 1200;
                    this.advance = 150;
                }
                else if (mediumComplexityChecked == true)
                {
                    this.cost = 1500;
                    this.advance = 150;
                }
                else if (HardChecked == true)
                {
                    this.cost = 2000;
                    this.advance = 150;
                }
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
                    return stream;
                });
                IsVisible = true;
            }
        }
        #endregion

    }
}
