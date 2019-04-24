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
        private Image image;

        private bool isRefreshing;
        private bool isRunning;

        private ObservableCollection<TecnicoItemViewModel> tecnicos;
        private ObservableCollection<TrabajosItemViewModel> trabajos;


        private T_clientes cliente;
        private T_usuarios user;
        private T_tecnicos tecnico;
        private T_trabajos trabajo;

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
        public List<T_trabajos> TrabajoList { get; set; }
        public List<T_trabajocitas> CitasList { get; set; }

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
        public T_trabajos Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public ObservableCollection<TrabajosItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }

        public byte[] ByteImage
        {
            get { return this.byteImage; }
            set { SetValue(ref this.byteImage, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public Image Image
        {
            get { return this.image; }
            set { SetValue(ref this.image, value); }
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
        public TecnicoHomeViewModel(T_usuarios user, T_tecnicos tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();

            this.LoadTecnico();
            this.LoadTrabajos();

            this.IsRunning = false;
            this.IsRefreshing = false;
            this.TipoBusqueda = "All";

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
        public ICommand RefreshTrabajoCommand
        {
            get
            {
                return new RelayCommand(LoadTrabajos);
            }
        }
        #endregion

        #region Methods

        private void LoadTecnico()
        {
            this.IsRefreshing = true;

            //if (this.tecnico.F_Perfil != null)
            //{
            //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TecnicoIcon.png");
            //    File.WriteAllBytes(fileName, this.tecnico.F_Perfil);
            //    this.Image = new Image();
            //    this.ImageSource = FileImageSource.FromFile(fileName);
            //}
            //else
            //{
            //    this.ByteImage = this.apiService.GetImageFromFile("mLetsTatoo.NoUserPic.png");
            //    this.ImageSource = ImageSource.FromStream(() => new MemoryStream(this.ByteImage));
            //}

            this.IsRefreshing = false;
        }
        private async void LoadTrabajos()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;

            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.GetList<T_trabajos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.TrabajoList = (List<T_trabajos>)response.Result;

            var trabajo = this.TrabajoList.Select(c => new TrabajosItemViewModel
            {
                
                Id_Trabajo = c.Id_Trabajo,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                Asunto = c.Asunto,
                Costo_Cita = c.Costo_Cita,
                Total_Aprox = c.Total_Aprox,
                Id_Caract = c.Id_Caract,

            }).Where(c => c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            this.Trabajos = new ObservableCollection<TrabajosItemViewModel>(trabajo.OrderBy(c => c.Id_Trabajo));

            this.IsRefreshing = false;
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
        #endregion
    }
}
