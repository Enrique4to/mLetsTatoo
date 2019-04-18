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
        private ObservableCollection<TecnicoTrabajosItemViewModel> trabajos;
        private ObservableCollection<CitasItemViewModel> citas;


        private T_clientes cliente;
        private T_usuarios user;
        private T_tecnicos tecnico;
        private T_trabajos trabajo;
        private T_trabajocitas cita;

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
        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }

        public ObservableCollection<TecnicoItemViewModel> Tecnicos
        {
            get { return this.tecnicos; }
            set { SetValue(ref this.tecnicos, value); }
        }
        public ObservableCollection<TecnicoTrabajosItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
        }
        public ObservableCollection<CitasItemViewModel> Citas
        {
            get { return this.citas; }
            set { SetValue(ref this.citas, value); }
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
            
            this.IsRunning = false;
            this.IsRefreshing = false;
            this.TipoBusqueda = "All";

            this.LoadTecnico();
            Task.Run(async () => { await this.LoadTrabajos(); }).Wait();
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
                return new RelayCommand(RefreshTrabajoList);
            }
        }
        #endregion
        #region Methods

        private void LoadTecnico()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

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
        private async Task LoadTrabajos()
        {
            this.IsRefreshing = true;
            this.IsRunning = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsRunning = false;
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
                this.IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.TrabajoList = (List<T_trabajos>)response.Result;

            var trabajo = this.TrabajoList.Select(c => new TecnicoTrabajosItemViewModel
            {
                
                Id_Trabajo = c.Id_Trabajo,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                Asunto = c.Asunto,
                Costo_Cita = c.Costo_Cita,
                Total_Aprox = c.Total_Aprox,
                Id_Caract = c.Id_Caract,

            }).Where(c => c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            this.Trabajos = new ObservableCollection<TecnicoTrabajosItemViewModel>(trabajo.OrderBy(c => c.Id_Trabajo));

            controller = App.Current.Resources["UrlT_trabajocitasController"].ToString();

            response = await this.apiService.GetList<T_trabajocitas>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            this.CitasList = (List<T_trabajocitas>)response.Result;

            CitasList = CitasList.Where(c => c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            var cita = CitasList.Select(c => new CitasItemViewModel
            {
                Asunto = c.Asunto,
                Completa = c.Completa,
                F_Fin = c.F_Fin,
                H_Fin = c.H_Fin,
                F_Inicio = c.F_Inicio,
                H_Inicio = c.H_Inicio,
                Id_Cliente = c.Id_Cliente,
                Id_Tatuador = c.Id_Tatuador,
                Id_Cita = c.Id_Cita,
                Id_Trabajo = c.Id_Trabajo,

            }).Where(c => c.Id_Trabajo == this.trabajo.Id_Trabajo).ToList();
            this.Citas = new ObservableCollection<CitasItemViewModel>(cita.OrderBy(c => c.F_Inicio));

            this.IsRefreshing = false;
            this.IsRunning = false;

        }
        public void RefreshTrabajoList()
        {
            Task.Run(async () => { await this.LoadTrabajos(); }).Wait();
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
