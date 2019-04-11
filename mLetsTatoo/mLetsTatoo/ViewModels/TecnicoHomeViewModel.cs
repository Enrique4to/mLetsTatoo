namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Plugin.Media.Abstractions;
    using Services;
    using Views;
    using Xamarin.Forms;
    public class TecnicoHomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private byte[] byteImage;
        private ImageSource imageSource;
        private bool isRefreshing;
        private bool isRunning;
        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private T_clientes cliente;
        private T_usuarios user;
        private T_tecnicos tecnico;
        private Image image;
        private string file;
        public string filter;
        #endregion

        #region Properties
        public string TipoBusqueda { get; set; }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
            }
        }
        public List<T_tecnicos> TecnicoList { get; set; }
        public T_clientes Cliente
        {
            get { return this.cliente; }
            set { SetValue(ref this.cliente, value); }
        }
        public T_usuarios User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        public T_tecnicos Tecnico
        {
            get { return this.tecnico; }
            set { SetValue(ref this.tecnico, value); }
        }
        public Image Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
        }
        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
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
        #endregion

        #region Constructors
        public TecnicoHomeViewModel(T_usuarios user)
        {
            this.user = user;
            this.apiService = new ApiService();
            
            this.IsRunning = false;
            this.IsRefreshing = false;
            this.TipoBusqueda = "All";

            Task.Run(async () => { await this.LoadTecnico(); }).Wait();
        }
        #endregion
        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Busqueda);
            }
        }
        public ICommand NewAppointmentCommand
        {
            get
            {
                return new RelayCommand(GoToCitasPage);
            }
        }
        #endregion
        #region Methods

        private async Task LoadTecnico()
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
            var controller = Application.Current.Resources["UrlT_tecnicosController"].ToString();

            var response = await this.apiService.GetList<T_tecnicos>(urlApi, prefix, controller);
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
            var listtecnico = (List<T_tecnicos>)response.Result;

            this.tecnico = listtecnico.Single(t => t.Id_Usuario == this.user.Id_usuario);
            if (this.tecnico.F_Perfil != null)
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TecnicoIcon.png");
                File.WriteAllBytes(fileName, this.tecnico.F_Perfil);
                this.Image = new Image();
                this.ImageSource = FileImageSource.FromFile(fileName);
            }
            else
            {
                this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
                this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            }


            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        public void Busqueda()
        {
            if (TipoBusqueda == "All")
            {
            }
            if (TipoBusqueda == "Citas")
            {
            }
        }
        private async void GoToCitasPage()
        {
            MainViewModel.GetInstance().NewDate = new NewDateViewModel(tecnico, user, cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new NewDatePage());
        }
        #endregion
    }
}
