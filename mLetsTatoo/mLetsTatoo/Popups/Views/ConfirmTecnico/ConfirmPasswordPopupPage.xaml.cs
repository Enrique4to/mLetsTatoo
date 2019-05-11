

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmPasswordPopupPage : PopupPage
	{
		public ConfirmPasswordPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
		}
	}
}