namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class EmpresaItemViewModel : T_empresas
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public T_clientes cliente;
        public T_usuarios user;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public EmpresaItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand EmpresaPageCommand
        {
            get
            {
                return new RelayCommand(GoToEmpresaPage);
            }
        }
        #endregion

        #region Methods
        private async void GoToEmpresaPage()
        {
            this.user = MainViewModel.GetInstance().UserHome.User;
            this.cliente = MainViewModel.GetInstance().UserHome.Cliente;
            MainViewModel.GetInstance().Empresa = new EmpresaViewModel(this, user, cliente);
            //Application.Current.MainPage = new NavigationPage(new EmpresaPage())
            //{
            //    BarBackgroundColor = Color.FromRgb(20, 20, 20),
            //    BarTextColor = Color.FromRgb(200, 200, 200),
            //};
            await Application.Current.MainPage.Navigation.PushModalAsync(new EmpresaPage());
            //await Application.Current.MainPage.Navigation.PushModalAsync(new EmpresaPage());

        }
        #endregion
    }
}
