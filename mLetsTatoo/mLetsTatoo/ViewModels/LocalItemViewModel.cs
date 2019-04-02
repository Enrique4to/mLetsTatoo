namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Views;
    using Xamarin.Forms;

    public class LocalItemViewModel : T_locales
    {
        #region Services
        private ApiService apiService;
        private T_empresas empresa;
        #endregion

        #region Attributes
        #endregion

        #region Constructors
        public LocalItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand LocalPageCommand
        {
            get
            {
                return new RelayCommand(GoToLocalPage);
            }
        }
        #endregion

        #region Methods
        private async void GoToLocalPage()
        {
            this.empresa = MainViewModel.GetInstance().Empresa.empresa;
            MainViewModel.GetInstance().LocalPage = new LocalViewModel(this, empresa);
            await Application.Current.MainPage.Navigation.PushAsync(new LocalPage());
        }
        #endregion
    }
}
