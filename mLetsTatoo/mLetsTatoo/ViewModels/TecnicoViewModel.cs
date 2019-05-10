namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using Models;
    using Rg.Plugins.Popup.Extensions;
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

        public List<T_tecimagenes> tecnicoImagList;

        private ObservableCollection<T_tecimagenes> imagenes;


        #endregion

        #region Properties
        public List<T_tecimagenes> TecnicoImagList
        {
            get { return this.tecnicoImagList; }
            set { SetValue(ref this.tecnicoImagList, value); }
        }

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

        public ObservableCollection<T_tecimagenes> Imagenes
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
        }
        #endregion

        #region Commands
        public ICommand NewAppointmentCommand
        {
            get
            {
                return new RelayCommand(GoToCitasPage);
            }
        }
        #endregion

        #region Methods

        private async void GoToCitasPage()
        {
            MainViewModel.GetInstance().NewAppointmentPopup = new NewAppointmentPopupViewModel(this.cliente);
            MainViewModel.GetInstance().NewAppointmentPopup.fromTecnitoPage = true;
            MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this.tecnico;
            MainViewModel.GetInstance().NewAppointmentPopup.thisPage = "Type";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new TypeAppointmentPopupPage());
        }
        private void LoadTecnico()
        {
            this.selectedArtist = $"{this.tecnico.Apodo} - {this.tecnico.Nombre} {this.tecnico.Apellido}";
            this.NombreCompleto = $"{this.tecnico.Nombre} {this.tecnico.Apellido}";

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
        public void LoadImagenes()
        {
            var tecnicoImagSelected = this.TecnicoImagList.Select(li => new T_tecimagenes
            {
                Id_Imagen = li.Id_Imagen,
                Id_Tecnico = li.Id_Tecnico,
                Imagen = li.Imagen,
                Descripcion = li.Descripcion,

            }).Where(li => li.Id_Tecnico == this.tecnico.Id_Tecnico).ToList();

            this.Imagenes = new ObservableCollection<T_tecimagenes>(tecnicoImagSelected.OrderBy(li => li.Id_Imagen));
        }

        #endregion
    }
}
