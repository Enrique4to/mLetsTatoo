namespace mLetsTatoo.Views
{
    using mLetsTatoo.ViewModels;
    using Syncfusion.XForms.Buttons;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    public partial class NewDatePage : ContentPage
	{
        #region Attributes
        public decimal cost;
        public decimal advance;
        public bool pageVisible;
        #endregion

        public NewDatePage ()
        {
            this.pageVisible = true;
            InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent ();
        }
        private void LoadFeaturesPage(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                var pageButton = (SfRadioButton)sender;
                if (pageButton == this.QuickApp)
                {
                    MainViewModel.GetInstance().NewDate.pageVisible = true;
                    this.pageVisible = true;
                    this.QuickPage.IsVisible = true;
                    this.QuickApp.TextColor = Color.LightGray;
                    this.PersonalizedApp.TextColor = Color.Gray;
                }
                else if (pageButton == this.PersonalizedApp)
                {
                    MainViewModel.GetInstance().NewDate.pageVisible = false;
                    this.pageVisible = false;
                    this.QuickPage.IsVisible = false;
                    this.PersonalizedApp.TextColor = Color.LightGray;
                    this.QuickApp.TextColor = Color.Gray;
                }
            }

        }
        private void LoadFeatures(object sender, StateChangedEventArgs e)
        {
            if(this.pageVisible == true)
            {
                if (MainViewModel.GetInstance().NewDate.tecnico != null)
                {
                    if (e.IsChecked.HasValue && e.IsChecked.Value)
                    {
                        MainViewModel.GetInstance().NewDate.LoadFeatures(sender);
                    }
                }
            }
        }
    }
}