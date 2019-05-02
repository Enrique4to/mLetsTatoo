namespace mLetsTatoo.Views
{
    using ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Syncfusion.XForms.Buttons;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMessajeJobPage : TabbedPage
    {
        public UserMessajeJobPage()
		{
			InitializeComponent ();
            this.CurrentPage = this.Messages;
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
            this.CurrentPageChanged += (object sender, EventArgs e) =>
            {

                var i = this.Children.IndexOf(this.CurrentPage);

                this.JobInfo.Icon = "InfoUns.png";
                this.Messages.Icon = "CitasUns.png";

                switch (i)
                {
                    case 0:
                        this.Messages.Icon = "Citas.png";
                        break;
                    case 1:
                        this.JobInfo.Icon = "info.png";
                        break;
                }

            };
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
    }
}