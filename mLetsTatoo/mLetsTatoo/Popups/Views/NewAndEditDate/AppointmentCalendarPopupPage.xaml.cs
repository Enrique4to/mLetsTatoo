namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppointmentCalendarPopupPage : PopupPage
    {
        #region Constructors
        public AppointmentCalendarPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
        }
        #endregion

        #region Methods
        #endregion
    }
}