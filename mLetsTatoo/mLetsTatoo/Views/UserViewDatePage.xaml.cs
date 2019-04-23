namespace mLetsTatoo.Views
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserViewDatePage : ContentPage
    {
        private Grid _grid;
        public UserViewDatePage ()
		{
			InitializeComponent ();
		}
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
    }
}