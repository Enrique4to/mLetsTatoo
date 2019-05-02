namespace mLetsTatoo.Views
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using ViewModels;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public partial class TecnicoHomePage : TabbedPage
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
                if (MainViewModel.GetInstance().TecnicoHome.TipoBusqueda == "All")
                {
                    MainViewModel.GetInstance().TecnicoHome.filter = this.filter;
                }
                if (MainViewModel.GetInstance().TecnicoHome.TipoBusqueda == "Citas")
                {
                    MainViewModel.GetInstance().TecnicoHome.filter = this.filter;
                }
            }
        }
        #endregion
        #region Constructors
        public TecnicoHomePage ()
		{
			InitializeComponent ();
            this.CurrentPage = this.Citas;
            this.Search.BackgroundColor = Color.FromRgb(50, 50, 50);
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
            this.CurrentPageChanged += (object sender, EventArgs e) =>
            {

                var i = this.Children.IndexOf(this.CurrentPage);

                MainViewModel.GetInstance().TecnicoHome.TipoBusqueda = "All";
                this.News.Icon = "NewsFeedUns.png";
                this.Citas.Icon = "CitasUns.png";
                this.Notifications.Icon = "NotificacionUns.png";
                this.TecnicoOptions.Icon = "OptionsUns.png";
                this.Search.Placeholder = Languages.Search;

                switch (i)
                {
                    case 0:
                        this.News.Icon = "NewsFeed.png";
                        break;
                    case 1:
                        this.Citas.Icon = "Citas.png";
                        this.Search.Placeholder = Languages.SearchAppointment;
                        MainViewModel.GetInstance().TecnicoHome.TipoBusqueda = "Citas";
                        break;
                    case 2:
                        this.Notifications.Icon = "Notificacion.png";
                        break;
                    case 3:
                        this.TecnicoOptions.Icon = "Options.png";
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
                return new RelayCommand(MainViewModel.GetInstance().TecnicoHome.GoToMessagesPage);
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
            if (MainViewModel.GetInstance().TecnicoHome.TipoBusqueda == "All")
            {
                MainViewModel.GetInstance().TecnicoHome.filter = this.filter;
            }
            if (MainViewModel.GetInstance().TecnicoHome.TipoBusqueda == "Citas")
            {
                MainViewModel.GetInstance().TecnicoHome.filter = this.filter;
            }
        }
        #endregion


    }

}
