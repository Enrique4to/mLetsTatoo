namespace mLetsTatoo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Models;
    using mLetsTatoo.Services;
    using mLetsTatoo.Views;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NotasTempItemViewModel : TrabajonotaTempCollection
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Constructors
        public NotasTempItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand SelectedNotaCommand
        {
            get
            {
                return new RelayCommand(SelectedNota);
            }
        }
        #endregion

        #region Methods
        private void SelectedNota()
        {
            if (MainViewModel.GetInstance().UserMessageJob != null)
            {
                if (MainViewModel.GetInstance().UserMessageJob.user.Tipo != this.Tipo_Usuario)
                {
                    if(this.Propuesta == true)
                    {
                        MainViewModel.GetInstance().UserMessageJob.tecnico = MainViewModel.GetInstance().UserHome.Tecnicos.Single(t => t.Id_Usuario == this.Id_Usuario);
                        MainViewModel.GetInstance().UserMessageJob.notaSelected = this;
                        MainViewModel.GetInstance().UserMessageJob.LoadBudget();
                    }
                }
            }
        }
        #endregion
    }
}
