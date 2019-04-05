namespace mLetsTatoo.Views
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.ViewModels;
    using Xamarin.Forms;
    public partial class SearchPage : ContentPage
	{
        #region Attributes
        public string filter;
        #endregion

        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;

                    MainViewModel.GetInstance().UserHome.filterTecnico = this.filter;
                    MainViewModel.GetInstance().UserHome.RefreshTecnicoList();
            }
        }
        #endregion

        #region Constructors
        public SearchPage()
        {
            InitializeComponent();
        } 
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Busqueda);
            }
        }
        #endregion

        #region Methods
        private void Busqueda()
        {
                MainViewModel.GetInstance().UserHome.filterTecnico = this.filter;
                MainViewModel.GetInstance().UserHome.RefreshTecnicoList();
        }
        #endregion
    }
}