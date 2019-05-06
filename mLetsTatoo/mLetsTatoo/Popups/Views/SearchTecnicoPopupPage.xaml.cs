namespace mLetsTatoo.Popups.Views
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Pages;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchTecnicoPopupPage : PopupPage
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
        public SearchTecnicoPopupPage()
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

        private Frame _grid;
        private void OnFrameSelect(object s, EventArgs e)
        {
            if (_grid != null)
            {
                _grid.BackgroundColor = Color.Black;
            }

            var button = (Frame)s;
            button.BackgroundColor = Color.FromHex("#434343");
            _grid = button;
        }
        #endregion
    }
}