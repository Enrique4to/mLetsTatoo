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
        private void GoToTecnicoPage()
        {
            user = MainViewModel.GetInstance().UserHome.User;
            cliente = MainViewModel.GetInstance().UserHome.Cliente;
            MainViewModel.GetInstance().Tecnico = new TecnicoViewModel(this, user, cliente);
            Application.Current.MainPage = new NavigationPage(new TecnicoPage())
            {
                BarBackgroundColor = Color.FromRgb(20, 20, 20),
                BarTextColor = Color.FromRgb(200, 200, 200),
            };
            //await Application.Current.MainPage.Navigation.PushAsync(new TecnicoPage());
        }
        private async void TecnicoSelected()
        {
            MainViewModel.GetInstance().CitasPage = new CitasViewModel(this);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion
    }
}
