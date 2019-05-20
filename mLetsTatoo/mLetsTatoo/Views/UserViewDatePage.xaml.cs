namespace mLetsTatoo.Views
{
    using Popups.Views;
    using Rg.Plugins.Popup.Extensions;
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserViewDatePage : ContentPage
    {
        public UserViewDatePage ()
		{
			InitializeComponent ();
		}
        private Grid _grid;
        private void OnGridSelect(object s, EventArgs e)
        {
            if (_grid != null)
            {
                _grid.BackgroundColor = Color.Transparent;
            }

            var button = (Grid)s;
            button.BackgroundColor = Color.DimGray;
            _grid = button;
        }

        private void HideAndShow(object sender, EventArgs e)
        {
            if(this.Details.IsVisible != true)
            {
                this.Details.IsVisible = true;
                this.Cancel.IsVisible = true;
                this.Chevron.RotateTo(90, 60);
                return;
            }
            this.Details.IsVisible = false;
            this.Cancel.IsVisible = false;
            this.Chevron.RotateTo(0, 60);
        }

        private async void GoToCancelPopupPage(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().UserViewDate.cancelPage = "CancelDate";
            await Application.Current.MainPage.Navigation.PushPopupAsync(new CancelDatePopupPage());         
        }
    }
}