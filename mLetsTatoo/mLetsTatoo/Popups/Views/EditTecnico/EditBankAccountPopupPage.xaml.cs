

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditBankAccountPopupPage : PopupPage
	{
		public EditBankAccountPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
		}

        private void ShowPicker(object sender, System.EventArgs e)
        {
            this.picker.IsOpen = true;
        }
    }
}