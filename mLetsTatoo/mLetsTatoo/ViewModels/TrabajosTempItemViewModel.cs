namespace mLetsTatoo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class TrabajosTempItemViewModel : TrabajosTempCollection
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ClientesCollection cliente;
        private TecnicosCollection tecnico;
        private T_usuarios user;
        #endregion

        #region Properties
        public List<T_trabajonotatemp> TrabajoNotaList { get; set; }
        #endregion

        #region Constructors
        public TrabajosTempItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand MessageJobPageCommand
        {
            get
            {
                return new RelayCommand(GoToMessageJobPage);
            }
        }

        #endregion

        #region Methods
        private void GoToMessageJobPage()
        {
            this.user = MainViewModel.GetInstance().Login.user;
            if(MainViewModel.GetInstance().Login.tecnico != null)
            {
                this.tecnico = MainViewModel.GetInstance().Login.tecnico;
                this.TrabajoNotaList = MainViewModel.GetInstance().TecnicoMessages.TrabajoNotaList;
                MainViewModel.GetInstance().TecnicoMessageJob = new TecnicoMessageJobViewModel(this, this.tecnico, this.user, this.TrabajoNotaList);
                Application.Current.MainPage.Navigation.PushModalAsync(new TecnicoMessajeJobPage());
            }
            else if(MainViewModel.GetInstance().Login.cliente != null)
            {
                this.cliente = MainViewModel.GetInstance().Login.cliente;
                this.TrabajoNotaList = MainViewModel.GetInstance().UserMessages.TrabajoNotaList;
                MainViewModel.GetInstance().UserMessageJob = new UserMessageJobViewModel(this, this.cliente, this.user, this.TrabajoNotaList);
                Application.Current.MainPage.Navigation.PushModalAsync(new UserMessajeJobPage());
            }
        }
        #endregion
    }
}
