namespace mLetsTatoo.Views
{
    using mLetsTatoo.Popups.ViewModel;
    using mLetsTatoo.Popups.Views;
    using mLetsTatoo.ViewModels;
    using Rg.Plugins.Popup.Extensions;

    using Xamarin.Forms;
    public partial class StartPage : ContentPage
	{
		public StartPage()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTAxNjlAMzEzNzJlMzEyZTMwRjQwcisrVWozVTBVMStqRUJjaFY4ckJta3NqaWdRdkpjVzJHVm5NSkwzQT0=");
            InitializeComponent ();

            MainViewModel.GetInstance().Login.LoadLists();
        }
	}
}