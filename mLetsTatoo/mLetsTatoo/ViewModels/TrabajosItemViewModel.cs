namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class TrabajosItemViewModel : T_trabajos
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private T_trabajocitas cita;

        private ObservableCollection<CitasItemViewModel> citas;
        #endregion

        #region Properties
        public List<T_trabajocitas> CitasList { get; set; }

        public ObservableCollection<CitasItemViewModel> Citas
        {
            get { return this.citas; }
            set { this.citas = value; }
        }

        #endregion

        #region Constructors
        public TrabajosItemViewModel()
        {
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands
        public ICommand TecnicoViewJobPageCommand
        {
            get
            {
                return new RelayCommand(GoToTecnicoViewJobPage);
            }
        }
        //public ICommand TecnicoSelectedCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(TecnicoSelected);
        //    }
        //}
        #endregion

        #region Methods
        public void GoToTecnicoViewJobPage()
        {
            var user = MainViewModel.GetInstance().Login.user;
            var tecnico = MainViewModel.GetInstance().Login.tecnico;
            MainViewModel.GetInstance().TecnicoViewJob = new TecnicoViewJobViewModel(this, user, tecnico);
            Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoViewJobPage());
        }
        //private async void TecnicoSelected()
        //{
        //    MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //}
        #endregion
    }
}
