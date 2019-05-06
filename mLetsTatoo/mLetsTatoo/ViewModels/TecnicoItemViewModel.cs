namespace mLetsTatoo.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using Models;
    using Services;
    using Xamarin.Forms;
    using System.Collections.ObjectModel;

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
            MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
            await Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoPage());
        }
        private void TecnicoSelected()
        {
            //MainViewModel.GetInstance().NewDate = new NewDateViewModel(this, user, cliente);
            MainViewModel.GetInstance().NewAppointmentPopup.tecnico = this;
            //MainViewModel.GetInstance().NewDate.SelectedArtist = $"Artista: {this.Apodo} - {this.Nombre} {this.Apellido}";
            //await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        #endregion
    }
}
