

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCommentPopupPage : PopupPage
	{
		public EditCommentPopupPage ()
		{
			InitializeComponent ();
            this.Comment.Focus();
		}
	}
}