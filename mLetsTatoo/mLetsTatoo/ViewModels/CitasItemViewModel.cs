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
        //public ICommand TecnicoSelectedCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(TecnicoSelected);
        //    }
        //}
        #endregion

        #region Methods
        private async void GoToUserViewDatePage()
        {
            var user = MainViewModel.GetInstance().Login.user;
            var cliente = MainViewModel.GetInstance().Login.cliente;
            MainViewModel.GetInstance().UserViewDate = new UserViewDateViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new UserViewDatePage());
        }
        //private async void TecnicoSelected()
        //{
        //    MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //}
        #endregion
    }
}
