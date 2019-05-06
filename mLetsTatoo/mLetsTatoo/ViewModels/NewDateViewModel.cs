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
        private bool isRunning;
        private bool isEnabled;
        private bool isVisivle;

        private bool smallChecked;
        private bool mediumSizeChecked;
        private bool bigChecked;
        private bool easyChecked;
        private bool mediumComplexityChecked;
        private bool hardChecked;
        public bool pageVisible;

        public string filter;
        private string selectedArtist;
        private string describeArt;
        private string heightWidth;
        private string appCost;
        private string appAdvance;
        private string complexity;


        public TecnicosCollection tecnico;
        public T_usuarios user;
        public T_teccaract feature;
        public ClientesCollection cliente;
        public T_trabajos trabajo;
        public T_trabajocitas cita;
        public T_trabajonota nota;
        public T_citaimagenes notaImagen;
        public T_trabajostemp trabajotemp;
        public T_trabajonotatemp notatemp;
        public T_citaimagenestemp notaImagentemp;

        public decimal cost;
        public decimal advance;
        public decimal height;
        public decimal width;

        private byte[] byteImage;

        private ImageSource imageSource;
        private ImageSource imageSource2;

        private DateTime appointmentDate;
        private TimeSpan appointmentTime;

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
        public decimal Height
        {
            get { return this.height; }
            set { SetValue(ref this.height, value); }
        }
        public decimal Width
        {
            get { return this.width; }
            set { SetValue(ref this.width, value); }
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

        public DateTime MinDate { get; set; }
        public DateTime AppointmentDate
        {
            get { return this.appointmentDate; }
            set { SetValue(ref this.appointmentDate, value); }
        }
        public TimeSpan AppointmentTime
        {
            get { return this.appointmentTime; }
            set
            {
                SetValue(ref this.appointmentTime, value);
            }
        }

        public List<T_teccaract> ListFeature { get; set; }
        public List<T_trabajos> ListJob { get; set; }
        #endregion

        #region Constructors
        public NewDateViewModel(TecnicosCollection tecnico, T_usuarios user, ClientesCollection cliente)
        {
            this.tecnico = tecnico;
            this.user = user;
            this.cliente = cliente;

            this.apiService = new ApiService();

            this.smallChecked = true;
            this.easyChecked = true;

            this.pageVisible = true;

            this.MinDate = DateTime.Now.ToLocalTime();

            if (MainViewModel.GetInstance().Tecnico != null)
            {
                this.selectedArtist = MainViewModel.GetInstance().Tecnico.SelectedArtist;

            }
            else
            {
                this.selectedArtist = Languages.SelectArtist;
            }

            //if (this.tecnico != null)
            //{
            //    this.selectedArtist = $"Artista: {this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido}";
            //}
            this.apiService.EndActivityPopup();
            this.IsEnabled = true;
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
            //MainViewModel.GetInstance().Search = new SearchTecnicoPopupViewModel();
            //await Application.Current.MainPage.Navigation.PushModalAsync(new SearchPage());
        }
        private void SaveDate()
        {
            if(this.pageVisible == true)
            {
                this.SaveQuickDate();
            }
            else
            {
                this.SavePersonalDate();
            }
        }
        private async void SavePersonalDate()
        {

            if (this.selectedArtist == Languages.SelectArtist)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.SelectedArtistError,
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
            if (this.height < 0 || this.width < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                Languages.SizeError,
                "Ok");
                return;
            }

            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var subject = $"{Languages.Tattoo} {this.cliente.Nombre} {this.cliente.Apellido}, {Languages.Personalized}";

            this.trabajotemp = new T_trabajostemp
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                Asunto = subject,
                Alto = this.Height,
                Ancho = this.Width,
                Total_Aprox = 0,
                Costo_Cita = 0,
                Tiempo = 0,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajostempController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, this.trabajotemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.trabajotemp = (T_trabajostemp)response.Result;

            this.notatemp = new T_trabajonotatemp
            {
                Id_Trabajotemp = this.trabajotemp.Id_Trabajotemp,
                Tipo_Usuario= this.user.Tipo,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Nota = this.DescribeArt,
                F_nota = this.AppointmentDate,
                Nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}",                
            };

            controller = Application.Current.Resources["UrlT_trabajonotatempController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.notatemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.notatemp = (T_trabajonotatemp)response.Result;

            byte[] ByteImage = null;
            if (this.file == null)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage = FileHelper.ReadFully(this.file.GetStream());

            this.notaImagentemp = new T_citaimagenestemp
            {
                Imagen = ByteImage,
                Id_Trabajotemp = this.trabajotemp.Id_Trabajotemp,

            };
            controller = Application.Current.Resources["UrlT_citaimagenestempController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.notaImagentemp);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PopModalAsync();
            this.apiService.EndActivityPopup();

            var message = $"{Languages.TempJobMessageSent} {this.tecnico.Nombre} '{this.tecnico.Apodo}' {this.tecnico.Apellido}.{'\n'}{'\n'} {Languages.TempJobAnswerMessage}.";

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );

            
        }
        private async void SaveQuickDate()
        {
            if (this.selectedArtist == Languages.SelectArtist)
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
                Languages.TimeError,
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

            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var subject = $"Tatuaje de {this.feature.Alto} cm X {this.feature.Ancho} cm, {Languages.Complexity} {this.complexity}";

            this.trabajo = new T_trabajos
            {
                Id_Cliente = this.cliente.Id_Cliente,
                Id_Tatuador = this.tecnico.Id_Tecnico,
                Asunto = subject,
                Id_Caract = this.feature.Id_Caract,
                Total_Aprox = this.feature.Total_Aprox,
                Costo_Cita = this.feature.Costo_Cita,
                Alto = this.feature.Alto,
                Ancho = this.feature.Ancho,
                Tiempo = this.feature.Tiempo,
            };

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.Post(urlApi, prefix, controller, this.trabajo);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            this.trabajo = (T_trabajos)response.Result;

            var h_end = this.AppointmentTime.Add(TimeSpan.FromMinutes(this.feature.Tiempo));

            this.cita = new T_trabajocitas
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Id_Cliente = this.trabajo.Id_Cliente,
                Id_Tatuador = this.trabajo.Id_Tatuador,
                F_Inicio = this.AppointmentDate,
                F_Fin = this.AppointmentDate,
                H_Inicio = this.appointmentTime,
                H_Fin = h_end,
                Asunto = subject,
                Completa = false,
            };

            controller = Application.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.cita);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            this.cita = (T_trabajocitas)response.Result;

            var nombre_Post = $"{this.cliente.Nombre} {this.cliente.Apellido}";
            this.nota = new T_trabajonota
            {
                Id_Trabajo = this.trabajo.Id_Trabajo,
                Tipo_Usuario = 1,
                Id_Usuario = this.cliente.Id_Usuario,
                Id_Local = this.tecnico.Id_Local,
                Id_Cita = this.cita.Id_Cita,
                Nota = this.DescribeArt,
                Nombre_Post = nombre_Post,               
            };

            controller = Application.Current.Resources["UrlT_trabajonotaController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.nota);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }
            this.nota = (T_trabajonota)response.Result;

            byte[] ByteImage = null;
            if (this.file == null)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ExampleImageError,
                    "Ok");
                return;
            }
            ByteImage = FileHelper.ReadFully(this.file.GetStream());

            this.notaImagen = new T_citaimagenes
            {
                Imagen = ByteImage,
                Id_Trabajo = this.trabajo.Id_Trabajo,

            };
            controller = Application.Current.Resources["UrlT_citaimagenesController"].ToString();

            response = await this.apiService.Post(urlApi, prefix, controller, this.notaImagen);

            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();

                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                response.Message,
                "OK");
                return;
            }

            MainViewModel.GetInstance().UserHome.CitaList.Add(this.cita);
            MainViewModel.GetInstance().UserHome.RefreshCitaList();

            await Application.Current.MainPage.Navigation.PopModalAsync();
            this.apiService.EndActivityPopup();

            string date = DateTime.Parse(this.cita.F_Inicio.ToString()).ToString("dd-MMM-yyyy");
            string time = DateTime.Parse(this.cita.H_Inicio.ToString()).ToString("hh:mm tt");

            var message = $"{Languages.YourAppointment} #{this.trabajo.Id_Trabajo} {Languages.HasBeenCreated}  {date}  {Languages.At}  {time} " +
                $" {Languages.WithThe_Artist}  {this.tecnico.Nombre} '{this.tecnico.Apodo}' {this.tecnico.Apellido}.{'\n'}{'\n'} {Languages.TryToBe}.";

            await Application.Current.MainPage.DisplayAlert
                (Languages.Notice,
                message,
                "Ok"
                );
        }
        public async void LoadFeatures(object sender)
        {
            this.apiService.StartActivityPopup();
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.apiService.EndActivityPopup();
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
                this.apiService.EndActivityPopup();
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
                this.complexity = Languages.Easy;
            }
            else if (smallChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "SmallMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (smallChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "SmallHard");
                this.complexity = Languages.Hard;
            }

            if (mediumSizeChecked == true && easyChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumEasy");
                this.complexity = Languages.Easy;
            }
            else if (mediumSizeChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (mediumSizeChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "MediumHard");
                this.complexity = Languages.Hard;
            }

            if (bigChecked == true && easyChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigEasy");
                this.complexity = Languages.Easy;
            }
            else if (bigChecked == true && mediumComplexityChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigMedium");
                this.complexity = Languages.MediumComplexity;
            }
            else if (bigChecked == true && hardChecked == true)
            {
                this.feature = this.ListFeature.Single(f => f.Caract == "BigHard");
                this.complexity = Languages.Hard;
            }

            this.Cost = this.feature.Total_Aprox;
            this.Advance = this.feature.Costo_Cita;
            if (this.feature.Imagen_Ejemplo != null)
            {
                this.ImageSource2 = ImageSource.FromStream(() => new MemoryStream(this.feature.Imagen_Ejemplo));
            }
            this.HeightWidth = $"{Languages.MaximunSize} {this.feature.Alto} cm X {this.feature.Ancho} cm";
            this.AppCost = $"{Languages.Cost}: {this.feature.Total_Aprox.ToString("C2")}";
            this.AppAdvance = $"{Languages.Advance}: {this.feature.Costo_Cita.ToString("C2")}";
            
            this.apiService.EndActivityPopup();
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
