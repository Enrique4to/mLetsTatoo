using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class TecnicoProfilePage : ContentPage
	{
		public TecnicoProfilePage ()
		{
			InitializeComponent ();
            //this.FB.OnClickCommand = new Command(async () =>
            //{
            //    await DisplayAlert("Share", "Shared on Facebook!", "OK!");
            //});
            //this.TW.OnClickCommand = new Command(async () =>
            //{
            //    await DisplayAlert("Share", "Shared on Twitter!", "OK!");
            //});
            //this.TB.OnClickCommand = new Command(async () =>
            //{
            //    await DisplayAlert("Share", "Shared on Tumblr!", "OK!");
            //});
            //this.TD.OnClickCommand = new Command(async () =>
            //{
            //    await DisplayAlert("Share", "Shared on Tumblr!", "OK!");
            //});
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.layout.Children.Add(this.abex, () => new Rectangle(((this.layout.Width / 4) * 3) - (56 / 2), (this.layout.Height / 2) - (200), 56, 250));
            InitializeComponent();
        }
    }
}