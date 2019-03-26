namespace mLetsTatoo.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using ViewModels;
    using Xamarin.Forms;
    public class EmpresaViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;

        #endregion
        #region Attributes
        private ObservableCollection<T_empresas> empresas;
        private bool isRefreshing;
        #endregion
        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public ObservableCollection<T_empresas> Empresas
        {
            get { return this.empresas; }
            set { SetValue(ref this.empresas, value); }
        }
        #endregion
        #region Constructors
        public EmpresaViewModel()
        {
            this.apiService = new ApiService();
            this.IsRefreshing = false;
            this.LoadEmpresas();
        }
        #endregion
        #region Methods
        private async void LoadEmpresas()
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
            var controller = App.Current.Resources["UrlT_empresasController"].ToString();

            var response = await this.apiService.GetList<T_empresas>(urlApi, prefix, controller);
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            var list = (List<T_empresas>)response.Result;

            this.Empresas = new ObservableCollection<T_empresas>(list);
            this.IsRefreshing = false;
        }
        #endregion
        #region Commans
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadEmpresas);
            }
        }
        #endregion

    }
}
