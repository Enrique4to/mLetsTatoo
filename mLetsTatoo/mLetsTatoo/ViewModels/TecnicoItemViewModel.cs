namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Models;
    using Services;
    using Xamarin.Forms;
    using System.Collections.ObjectModel;

    public class TecnicoItemViewModel : T_tecnicos
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public T_tecnicos tecnico;
        public T_clientes cliente;
        public T_usuarios user;
        #endregion
        
        #region Constructors
        public TecnicoItemViewModel()
        {
            this.apiService = new ApiService();
            this.user = MainViewModel.GetInstance().UserHome.User;
            this.cliente = MainViewModel.GetInstance().UserHome.Cliente;
        }
        #endregion

        #region Commands
        public ICommand TecnicoPageCommand
        {
            get
            {
                return new RelayCommand(GoToTecnicoPage);
            }
        }
        public ICommand TecnicoSelectedCommand
        {
            get
            {
                return new RelayCommand(TecnicoSelected);
            }
        }
        #endregion

        #region Methods
        private async void GoToTecnicoPage()
        {
            MainViewModel.GetInstance().Tecnico = new TecnicoViewModel(this, user, cliente);
            MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoPage());
        }
        private async void TecnicoSelected()
        {
            MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion
    }
}
