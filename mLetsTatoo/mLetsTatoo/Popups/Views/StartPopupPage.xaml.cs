

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPopupPage : PopupPage
	{
		public StartPopupPage()
		{
            this.CloseWhenBackgroundIsClicked = false;
			InitializeComponent ();
		}

    }
}