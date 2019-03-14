namespace mLetsTatoo.ViewModels
{
    using ViewModels;
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
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            //this.Home = new HomeViewModel();
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
