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
        public TecnicoHomeViewModel TecnicoHome
        {
            get;
            set;
        }

        public EmpresaViewModel Empresa
        {
            get;
            set;
        }
        public RegisterViewModel Register
        {
            get;
            set;
        }
        public UserViewModel User
        {
            get;
            set;
        }
        public EditUserViewModel EditUser
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
