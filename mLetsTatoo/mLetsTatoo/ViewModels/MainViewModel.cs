namespace mLetsTatoo.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Views;
    using ViewModels;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region ViewModels

        public LoginViewModel Login
        {
            get;
            set;
        }
        public HomeViewModel Home
        {
            get;
            set;
        }

        public EmpresaViewModel Empresa
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            //this.Home = new HomeViewModel();
        }
        #endregion
        public ICommand HomePageCommand
        {
            get
            {
                return new RelayCommand(GoToHomePage);
            }
        }

        #region Methods
        private void GoToHomePage()
        {

            this.Empresa = new EmpresaViewModel();
            Application.Current.MainPage = new NavigationPage(new EmpresaPage())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.Gray,
            };
        } 
        #endregion
        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

    }

}
