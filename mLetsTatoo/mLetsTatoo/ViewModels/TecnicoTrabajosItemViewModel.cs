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

    public class TecnicoTrabajosItemViewModel : T_trabajos
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public TecnicoTrabajosItemViewModel()
        {
            this.apiService = new ApiService();
        }

        #endregion

        #region Commands
        public ICommand TecnicoViewDatePageCommand
        {
            get
            {
                return new RelayCommand(GoToTecnicoViewDatePage);
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
        private async void GoToTecnicoViewDatePage()
        {
            var user = MainViewModel.GetInstance().Login.user;
            var tecnico = MainViewModel.GetInstance().Login.tecnico;
            MainViewModel.GetInstance().TecnicoViewDate = new TecnicoViewDateViewModel(this, user, tecnico);
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoViewDatePage());
        }
        //private async void TecnicoSelected()
        //{
        //    MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //}
        #endregion
    }
}
