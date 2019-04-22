

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCommentPopupPage : PopupPage
	{
		public AddCommentPopupPage ()
		{
			InitializeComponent ();
            this.Comment.Focus();
		}
	}
}