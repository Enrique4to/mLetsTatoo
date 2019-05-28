

namespace mLetsTatoo.Popups.Views
{
    using Rg.Plugins.Popup.Pages;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSabadoPopupPage : PopupPage
	{
		public EditSabadoPopupPage()
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