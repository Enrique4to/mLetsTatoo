using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTAxNjlAMzEzNzJlMzEyZTMwRjQwcisrVWozVTBVMStqRUJjaFY4ckJta3NqaWdRdkpjVzJHVm5NSkwzQT0=");
            InitializeComponent ();
		}
	}
}