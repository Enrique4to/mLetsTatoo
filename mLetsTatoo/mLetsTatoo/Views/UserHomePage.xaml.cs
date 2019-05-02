
namespace mLetsTatoo.Views
{
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using mLetsTatoo.ViewModels;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public partial class UserHomePage : TabbedPage
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
                if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "All")
                {
                    MainViewModel.GetInstance().UserHome.filter = this.filter;
                }
                if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Studios")
                {
                    MainViewModel.GetInstance().UserHome.filterEmpresa = this.filter;
                    MainViewModel.GetInstance().UserHome.RefreshEmpresaList();
                }
                if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Citas")
                {
                    MainViewModel.GetInstance().UserHome.filter = this.filter;
                }
                if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Artists")
                {
                    MainViewModel.GetInstance().UserHome.filterTecnico = this.filter;
                    MainViewModel.GetInstance().UserHome.RefreshTecnicoList();
                }
            }
        } 
        #endregion

        #region Constructors
        public UserHomePage()
		{
            InitializeComponent ();

            this.Search.BackgroundColor = Color.FromRgb(50, 50, 50);
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor =Color.FromRgb(200,200,200);
            this.CurrentPageChanged += (object sender, EventArgs e) => 
            {

                var i = this.Children.IndexOf(this.CurrentPage);

                MainViewModel.GetInstance().UserHome.TipoBusqueda = "All";
                this.News.Icon = "NewsFeedUns.png";
                this.Citas.Icon = "CitasUns.png";
                this.Locals.Icon = "LocalUns.png";
                this.Artists.Icon = "TattooUns.png";
                this.Notifications.Icon = "NotificacionUns.png";
                this.UserOptions.Icon = "OptionsUns.png";
                this.Search.Placeholder = Languages.Search;

                switch (i)
                {
                    case 0:
                        this.News.Icon = "NewsFeed.png";
                        break;
                    case 1:
                        this.Citas.Icon = "Citas.png";
                        this.Search.Placeholder = Languages.SearchAppointment;
                        MainViewModel.GetInstance().UserHome.TipoBusqueda = "Citas";
                        break;
                    case 2:
                        this.Locals.Icon = "Local.png";
                        this.Search.Placeholder = Languages.SearchStudios;
                        MainViewModel.GetInstance().UserHome.TipoBusqueda = "Studios";
                        break;
                    case 3:
                        this.Artists.Icon = "Tattoo.png";
                        this.Search.Placeholder = Languages.SearchArtists;
                        MainViewModel.GetInstance().UserHome.TipoBusqueda = "Artists";
                        break;
                    case 4:
                        this.Notifications.Icon = "Notificacion.png";
                        break;
                    case 5:
                        this.UserOptions.Icon = "Options.png";
                        break;
                }

            };
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
        public ICommand MessagesCommand
        {
            get
            {
                return new RelayCommand(MainViewModel.GetInstance().UserHome.GoToMessagesPage);
            }
        }
        #endregion

        #region Methods        
        private Grid _grid;
        private void OnGridSelect(object s, EventArgs e)
        {
            if (_grid != null)
            {
                _grid.BackgroundColor = Color.Transparent;
            }

            var button = (Grid)s;
            button.BackgroundColor = Color.DimGray;
            _grid = button;
        }
        private void Busqueda()
        {
            if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "All")
            {
                MainViewModel.GetInstance().UserHome.filter = this.filter;
            }
            if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Studios")
            {
                MainViewModel.GetInstance().UserHome.filterEmpresa = this.filter;
                MainViewModel.GetInstance().UserHome.RefreshEmpresaList();
            }
            if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Citas")
            {
                MainViewModel.GetInstance().UserHome.filter = this.filter;
            }
            if (MainViewModel.GetInstance().UserHome.TipoBusqueda == "Artists")
            {
                MainViewModel.GetInstance().UserHome.filterTecnico = this.filter;
                MainViewModel.GetInstance().UserHome.RefreshTecnicoList();
            }
        }  
        #endregion

    }
}