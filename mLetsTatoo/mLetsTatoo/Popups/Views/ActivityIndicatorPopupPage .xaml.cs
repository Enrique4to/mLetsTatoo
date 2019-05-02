

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityIndicatorPopupPage : PopupPage
	{
		public ActivityIndicatorPopupPage()
		{
            this.CloseWhenBackgroundIsClicked = false;
			InitializeComponent ();
		}

    }
}