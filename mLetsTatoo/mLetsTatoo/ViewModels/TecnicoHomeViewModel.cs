namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class TecnicoHomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;

        private ObservableCollection<TrabajosItemViewModel> trabajos;


        private T_clientes cliente;
        private T_usuarios user;
        private TecnicosCollection tecnico;
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

        public List<T_trabajos> TrabajoList { get; set; }
        public List<T_trabajocitas> CitasList { get; set; }
        public List<T_tecnicohorarios> ListHorariosTecnicos { get; set; }

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
        public T_trabajos Trabajo
        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public ObservableCollection<TrabajosItemViewModel> Trabajos
        {
            get { return this.trabajos; }
            set { SetValue(ref this.trabajos, value); }
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
        public TecnicoHomeViewModel(T_usuarios user, TecnicosCollection tecnico)
        {
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();

            this.LoadTecnico();
            this.LoadTrabajos();
            
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

            MainViewModel.GetInstance().TecnicoProfile = new TecnicoProfileViewModel(this.user, this.tecnico);
            this.CitasList = MainViewModel.GetInstance().Login.CitaList;
            this.ListHorariosTecnicos = MainViewModel.GetInstance().Login.ListHorariosTecnicos;
            this.IsRefreshing = false;
        }
        private async void LoadTrabajos()
        {
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
            var controller = Application.Current.Resources["UrlT_trabajosController"].ToString();

            var response = await this.apiService.GetList<T_trabajos>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                this.apiService.EndActivityPopup();
                await Application.Current.MainPage.DisplayAlert(
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
                Alto = c.Alto,
                Ancho = c.Ancho,
                Tiempo = c.Tiempo,

            }).Where(c => c.Id_Tatuador == this.tecnico.Id_Tecnico).ToList();

            this.Trabajos = new ObservableCollection<TrabajosItemViewModel>(trabajo.OrderBy(c => c.Id_Trabajo));

            this.apiService.EndActivityPopup();
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
        public async void GoToMessagesPage()
        {
            this.apiService.StartActivityPopup();

            MainViewModel.GetInstance().TecnicoMessages = new TecnicoMessagesViewModel(this.user, this.tecnico);
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoMessagesPage());

            //this.apiService.EndActivityPopup();
        }
        #endregion
    }
}
