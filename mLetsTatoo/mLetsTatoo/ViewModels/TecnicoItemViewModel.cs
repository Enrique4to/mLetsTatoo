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
        #endregion

        #region Methods
        private async void GoToTecnicoPage()
        {
            user = MainViewModel.GetInstance().UserHome.User;
            cliente = MainViewModel.GetInstance().UserHome.Cliente;
            MainViewModel.GetInstance().Tecnico = new TecnicoViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PushAsync(new TecnicoPage());
        }
        #endregion
    }
}
