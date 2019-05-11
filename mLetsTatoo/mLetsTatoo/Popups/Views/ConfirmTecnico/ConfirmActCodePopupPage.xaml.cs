namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmActCodePopupPage : PopupPage
	{
		public ConfirmActCodePopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();            
		}
    }
}