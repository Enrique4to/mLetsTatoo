namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Models;
    using Services;
    using Xamarin.Forms;
    using System.Collections.ObjectModel;
    using mLetsTatoo.Helpers;
    using System.Collections.Generic;

    public class TecnicoItemViewModel : TecnicosCollection
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        public T_tecnicos tecnico;
        public ClientesCollection cliente;
        public T_usuarios user;
        #endregion
        
        #region Constructors
        public TecnicoItemViewModel()
        {
            this.apiService = new ApiService();
            this.user = MainViewModel.GetInstance().Login.user;
            this.cliente = MainViewModel.GetInstance().Login.cliente;
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

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    "OK");
                return;

            }

            var urlApi = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlT_tecimagenesController"].ToString();

            var response = await this.apiService.GetList<T_tecimagenes>(urlApi, prefix, controller);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    "OK");
                return;
            }
            MainViewModel.GetInstance().Tecnico.TecnicoImagList = (List<T_tecimagenes>)response.Result;

            MainViewModel.GetInstance().Tecnico.LoadImagenes();
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoPage());
        }
        private void TecnicoSelected()
        {
            MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this;
        }
        #endregion
    }
}
