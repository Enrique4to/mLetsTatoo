namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpres;
    using Models;
    using Services;
    using ViewModels;
    using Xamarin.Forms;

    public class HomeViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private ObservableCollection<Tusuarios> usuarios;
        private bool isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public ObservableCollection<Tusuarios> Usuarios
        {
            get { return this.usuarios; }
            set { SetValue(ref this.usuarios, value); }
        }
        #endregion
        #region Constructors
        public HomeViewModel()
        {
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.LoadUsuarios();
        }
        #endregion
        #region Methods
        private async void LoadUsuarios()
        {

            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;
            }

            var urlApi = App.Current.Resources["UrlAPI"].ToString();
            var prefix = App.Current.Resources["UrlPrefix"].ToString();
            var controller = App.Current.Resources["UrlTusuariosController"].ToString();

            var response = await this.apiService.GetList<Tusuarios>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var list = (List<Tusuarios>)response.Result;
            this.Usuarios = new ObservableCollection<Tusuarios>(list);
            this.IsRefreshing = false;
        }
        #endregion
        #region Commans
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadUsuarios);
            }
        }
        #endregion
    }
}
