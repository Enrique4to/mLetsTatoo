namespace mLetsTatoo.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels

        public LoginViewModel Login { get; set; }
        public UserHomeViewModel UserHome { get; set; }
        public TecnicoHomeViewModel TecnicoHome { get; set; }
        public EmpresaViewModel Empresa { get; set; }
        public RegisterViewModel Register { get; set; }
        public UserViewModel User { get; set; }
        public EditUserViewModel EditUser { get; set; }
        public TecnicoViewModel Tecnico { get; set; }
        public LocalViewModel Local { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
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
