namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Models;
    using mLetsTatoo.Services;
    using mLetsTatoo.Views;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Syncfusion.XForms.Buttons;
    using Xamarin.Forms;
    public class NewDateViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;
        private bool isEnabled;
        private bool isVisivle;
        private bool smallChecked;
        private bool mediumSizeChecked;
        private bool bigChecked;
        private bool easyChecked;
        private bool mediumComplexityChecked;
        private bool hardChecked;

        public string filter;
        private string selectedArtist;
        private string describeArt;
        private string heightWidth;
        private string appCost;
        private string appAdvance;

        public T_tecnicos tecnico;
        public T_usuarios user;
        public T_teccaract feature;
        public T_clientes cliente;

        public decimal cost;
        public decimal advance;

        private byte[] byteImage;

        private ImageSource imageSource;
        private ImageSource imageSource2;

        private MediaFile file;
        #endregion

        #region Properties
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
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
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
        public string HeightWidth
        {
            get { return this.heightWidth; }
            set { SetValue(ref this.heightWidth, value); }
        }
        public string AppCost
        {
            get { return this.appCost; }
            set { SetValue(ref this.appCost, value); }
        }
        public string AppAdvance
        {
            get { return this.appAdvance; }
            set { SetValue(ref this.appAdvance, value); }
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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public ImageSource ImageSource2
        {
            get { return this.imageSource2; }
            set { SetValue(ref this.imageSource2, value); }
        }

        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }

        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }

        public List<T_teccaract> ListFeature { get; set; }
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

            this.AppointmentDate = DateTime.Today;
            this.AppointmentTime = DateTime.Now;

            if (MainViewModel.GetInstance().Tecnico == null)
            {
                this.selectedArtist = Languages.SelectArtist;
            }
            else
            {
                this.selectedArtist = MainViewModel.GetInstance().Tecnico.SelectedArtist;
            }
            if (this.tecnico != null)
            {
                this.selectedArtist = $"Artista: {this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido1}";
            }

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
            if (string.IsNullOrEmpty(this.selectedArtist))
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
        public async void LoadFeatures(object sender)
        {
            this.IsRefreshing = true;
            this.IsRunning = true;
            
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_teccaractController"].ToString();

            var response = await this.apiService.GetList<T_teccaract>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var list = (List<T_teccaract>)response.Result;

            this.ListFeature = list.Where(f => f.Id_Tecnico == this.tecnico.Id_Tecnico).ToList();

            if (smallChecked == true && easyChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "SmallEasy");
            }
            else if (smallChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "SmallMedium");
            }
            else if (smallChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "SmallHard");
            }

            if (mediumSizeChecked == true && easyChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumEasy");
            }
            else if (mediumSizeChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumMedium");
            }
            else if (mediumSizeChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumHard");
            }

            if (bigChecked == true && easyChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigEasy");
            }
            else if (bigChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigMedium");
            }
            else if (bigChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigHard");
            }

            this.Cost = this.feature.Total_Aprox;
            this.Advance = this.feature.Costo_Cita;
            if (this.feature.Imagen_Ejemplo != null)
            {
                this.ImageSource2 = ImageSource.FromStream(() => new MemoryStream(this.feature.Imagen_Ejemplo));
            }
            this.HeightWidth = $"{Languages.MaximunSize} {this.feature.Alto} cm X {this.feature.Ancho} cm";
            this.AppCost = $"{Languages.ApproximateCost} {this.feature.Total_Aprox.ToString("C2")}";
            this.AppAdvance = $"{Languages.AppointmentCost} {this.feature.Costo_Cita.ToString("C2")}";

            this.IsRefreshing = false;
            this.IsRunning = false;
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
