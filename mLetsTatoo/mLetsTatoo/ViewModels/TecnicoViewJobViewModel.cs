namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class TecnicoViewJobViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRefreshing;
        private bool isRunning;

        private string subTotal;
        private string advance;
        private string total;
               
        private T_trabajocitas cita;
        private T_usuarios user;
        private ClientesCollection cliente;
        public T_trabajos trabajo;
        private TecnicosCollection tecnico;

        private ObservableCollection<CitasItemViewModel> citas;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public string SubTotal
        {
            get { return this.subTotal; }
            set { SetValue(ref this.subTotal, value); }
        }
        public string Advance
        {
            get { return this.advance; }
            set { SetValue(ref this.advance, value); }
        }
        public string Total
        {
            get { return this.total; }
            set { SetValue(ref this.total, value); }
        }
        
        public List<T_trabajocitas> CitasList { get; set; }

        public T_trabajocitas Cita
        {
            get { return this.cita; }
            set { SetValue(ref this.cita, value); }
        }
        public T_trabajos Trabajo        {
            get { return this.trabajo; }
            set { SetValue(ref this.trabajo, value); }
        }

        public ObservableCollection<CitasItemViewModel> Citas
        {
            get { return this.citas; }
            set { this.citas = value; }
        }
        #endregion

        #region Constructors
        public TecnicoViewJobViewModel(T_trabajos trabajo, T_usuarios user, TecnicosCollection tecnico)
        {
            this.trabajo = trabajo;
            this.user = user;
            this.tecnico = tecnico;
            this.apiService = new ApiService();
            this.RerfeshCitasList();
            //this.LoadCitas();
            this.LoadData();
            this.IsRefreshing = false;
            this.IsRunning = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshListCitasCommand
        {
            get
            {
                return new RelayCommand(RerfeshCitasList);
            }                
        }
        #endregion

        #region Methods
        public void LoadData()
        {
            this.subTotal = $"{Languages.SubTotal} {this.trabajo.Total_Aprox.ToString("C2")}";
            this.advance = $"{Languages.Advance} {this.trabajo.Costo_Cita.ToString("C2")}";
            var tot = this.trabajo.Total_Aprox - this.trabajo.Costo_Cita;
            this.total = $"{Languages.Remaining} {tot.ToString("C2")}";
        }

        public void RerfeshCitasList()
        {
            this.CitasList = MainViewModel.GetInstance().TecnicoHome.CitasList.Where(c => c.Id_Trabajo == this.trabajo.Id_Trabajo).ToList();

            var cita = this.CitasList.Select(c => new CitasItemViewModel
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
                ColorText = c.ColorText,
                Color = Color.FromHex(c.ColorText),
                CitaTemp = c.CitaTemp,
                CambioFecha = c.CambioFecha,
                TecnicoTiempo = c.TecnicoTiempo,
                Completado = MainViewModel.GetInstance().TecnicoHome.TrabajoList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Completo,
                Cancelado = MainViewModel.GetInstance().TecnicoHome.TrabajoList.FirstOrDefault(u => u.Id_Trabajo == c.Id_Trabajo).Cancelado,

            }).Where(c => c.Cancelado == false && c.CitaTemp == false && c.TecnicoTiempo == false).ToList();
            this.Citas = new ObservableCollection<CitasItemViewModel>(cita.OrderBy(c => c.F_Inicio));
        }

        #endregion

    }
}
