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

    public class CitasItemViewModel : T_trabajocitas
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        #endregion

        #region Properties
        public Color Color { get; set; }
        public bool Completado { get; set; }
        public bool Cancelado { get; set; }
        #endregion

        #region Constructors
        public CitasItemViewModel()
        {
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands
        public ICommand UserViewDatePageCommand
        {
            get
            {
                return new RelayCommand(GoToUserViewDatePage);
            }
        }
        public ICommand TecnicoViewDatePageCommand
        {
            get
            {
                return new RelayCommand(GoToTecnicoViewDatePage);
            }
        }
        #endregion

        #region Methods
        private void GoToUserViewDatePage()
        {
            var user = MainViewModel.GetInstance().Login.user;
            var cliente = MainViewModel.GetInstance().Login.cliente;
            MainViewModel.GetInstance().UserViewDate = new UserViewDateViewModel(this, user, cliente);
            Application.Current.MainPage.Navigation.PushModalAsync(new UserViewDatePage());
        }
        private void GoToTecnicoViewDatePage()
        {
            if(this.Completa != true)
            {
                var user = MainViewModel.GetInstance().Login.user;
                var tecnico = MainViewModel.GetInstance().Login.tecnico;
                var trabajo = MainViewModel.GetInstance().TecnicoViewJob.trabajo;
                MainViewModel.GetInstance().TecnicoViewDate = new TecnicoViewDateViewModel(this, user, tecnico, trabajo);
                Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoViewDatePage());
            }
        }
        //private async void TecnicoSelected()
        //{
        //    MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //}
        #endregion
    }
}
