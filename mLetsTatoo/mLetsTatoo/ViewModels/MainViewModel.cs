namespace mLetsTatoo.ViewModels
{
    using Interfaces;
    using Popups.ViewModel;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public UserHomeViewModel UserHome { get; set; }
        public TecnicoHomeViewModel TecnicoHome { get; set; }
        public EmpresaViewModel Empresa { get; set; }
        public RegisterAccountViewModel Register { get; set; }
        public UserViewModel UserPage { get; set; }
        public EditUserViewModel EditUser { get; set; }
        public EditTecnicoUserViewModel EditTecnicoUser { get; set; }
        public TecnicoViewModel Tecnico { get; set; }
        public LocalViewModel LocalPage { get; set; }
        public NewDateViewModel NewDate { get; set; }
        public TecnicoProfileViewModel TecnicoProfile { get; set; }
        public TecnicoConfirmViewModel TecnicoConfirm { get; set; }
        public TecnicoFeaturesViewModel TecnicoFeatures { get; set; }
        public TecnicoEditFeaturesViewModel TecnicoEditFeatures { get; set; }
        public UserViewDateViewModel UserViewDate { get; set; }
        public TecnicoViewJobViewModel TecnicoViewJob { get; set; }
        public TecnicoViewDateViewModel TecnicoViewDate { get; set; }
        public TecnicoMessagesViewModel TecnicoMessages { get; set; }
        public TecnicoMessageJobViewModel TecnicoMessageJob { get; set; }
        public UserMessagesViewModel UserMessages { get; set; }
        public UserMessageJobViewModel UserMessageJob { get; set; }
        #endregion

        #region PopupViewModels
        public AddCommentPopupViewModel AddCommentPopup { get; set; }
        public EditCommentPopupViewModel EditCommentPopup { get; set; }
        public ActivityIndicatorPopupViewModel ActivityIndicatorPopup { get; set; }
        public NewAppointmentPopupViewModel NewAppointmentPopup { get; set; }
        public StartPopupViewModel StartPopup { get; set; }
        public ConfirmTecnicoPopupViewModel ConfirmTecnicoPopup { get; set; }
        public NewPublicationPopupViewModel NewPublicationPopup { get; set; }
        public EditSchedulerPopupViewModel EditSchedulerPopup { get; set; }
        public EditFeaturesPopupViewModel EditFeaturesPopup { get; set; }
        public EditBankAccountPopupViewModel EditBankAccountPopup { get; set; }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
        }
        #endregion
        public void RergisterDevice()
        {
            var register = DependencyService.Get<IRegisterDevice>();
            register.RegisterDevice();
        }
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
