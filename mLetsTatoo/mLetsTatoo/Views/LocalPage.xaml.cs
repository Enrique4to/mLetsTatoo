namespace mLetsTatoo.Views
{
    using System.Threading.Tasks;
    using ViewModels;
    using Xamarin.Forms;

    public partial class LocalPage : TabbedPage
	{
		public LocalPage ()
        {
            InitializeComponent ();
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
        }
    }
}