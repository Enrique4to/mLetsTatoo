namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using mLetsTatoo.Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class TecnicoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion

        #region Attributes
        private string selectedArtist;
        private string nombreCompleto;
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRunning;
        public ClientesCollection cliente;
        public T_usuarios user;
        public TecnicosCollection tecnico;

        private ObservableCollection<T_localimagenes> imagenes;


        #endregion

        #region Properties
        public List<T_localimagenes> LocalImagList { get; set; }

        public ClientesCollection Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public TecnicosCollection Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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

        public string NombreCompleto
        {
            get { return this.nombreCompleto; }
            set { SetValue(ref this.nombreCompleto, value); }
        }
        public string SelectedArtist
        {
            get { return this.selectedArtist; }
            set { SetValue(ref this.selectedArtist, value); }
        }

        public ObservableCollection<T_localimagenes> Imagenes
        {
            get { return this.imagenes; }
            set { SetValue(ref this.imagenes, value); }
        }
        #endregion

        #region Constructors
        public TecnicoViewModel(TecnicosCollection tecnico, T_usuarios user, ClientesCollection cliente)
        {
            this.user = user;
            this.cliente = cliente;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.LoadTecnico();
            this.LoadImagenes();
        }
        #endregion

        #region Commands
        #endregion
        
        #region Methods
        private void LoadTecnico()
        {
            this.selectedArtist = $"{this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido1}";
            this.NombreCompleto = $"{this.tecnico.Nombre} {this.tecnico.Apellido1}";

            if (this.tecnico.F_Perfil != null)
            {
                ByteImage = this.tecnico.F_Perfil;
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.tecnico.F_Perfil));
            }
            else
            {
                this.ByteImage = apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }
        }
        private async void LoadImagenes()
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
            var controller = Application.Current.Resources["UrlT_localimagenesController"].ToString();

            var response = await this.apiService.GetList<T_localimagenes>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.LocalImagList = (List<T_localimagenes>)response.Result;

            var localImagSelected = this.LocalImagList.Select(li => new T_localimagenes
            {
                Id_Imagen = li.Id_Imagen,
                Id_Local = li.Id_Local,
                Imagen = li.Imagen,
                Descripcion = li.Descripcion,

            }).Where(li => li.Id_Local == this.tecnico.Id_Local).ToList();
            this.Imagenes = new ObservableCollection<T_localimagenes>(localImagSelected.OrderBy(li => li.Id_Imagen));

            this.IsRunning = false;

        }

        #endregion
    }
}
