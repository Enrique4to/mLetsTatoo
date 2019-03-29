using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor =Color.FromRgb(200,200,200);
		}
    }
}