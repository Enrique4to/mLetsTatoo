

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmDomingoPopupPage : PopupPage
	{
		public ConfirmDomingoPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
		}

        private void CheckedState(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                this.Comida.IsVisible = true;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                this.Comida.IsVisible = false;
            }
        }
    }
}