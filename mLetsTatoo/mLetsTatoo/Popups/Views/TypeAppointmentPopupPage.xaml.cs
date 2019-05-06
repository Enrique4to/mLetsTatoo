namespace mLetsTatoo.Popups.Views
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using mLetsTatoo.Helpers;
    using Rg.Plugins.Popup.Pages;
    using Syncfusion.XForms.Buttons;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TypeAppointmentPopupPage : PopupPage
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public TypeAppointmentPopupPage()
        {
            InitializeComponent();
            MainViewModel.GetInstance().NewAppointmentPopup.typeAppointment = true;
            this.Details.Text = Languages.QuickDetails;
        } 
        #endregion

        #region Commands
        #endregion

        #region Methods
        private void LoadFeaturesPage(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                var pageButton = (SfRadioButton)sender;
                if (pageButton == this.QuickApp)
                {
                    MainViewModel.GetInstance().NewAppointmentPopup.typeAppointment = true;
                    this.Details.Text = Languages.QuickDetails;
                    this.QuickApp.TextColor = Color.LightGray;
                    this.PersonalizedApp.TextColor = Color.Gray;
                }
                else if (pageButton == this.PersonalizedApp)
                {
                    MainViewModel.GetInstance().NewAppointmentPopup.typeAppointment = false;
                    this.Details.Text = Languages.PersonalizedDetails;
                    this.PersonalizedApp.TextColor = Color.LightGray;
                    this.QuickApp.TextColor = Color.Gray;
                }
            }

        }
        #endregion
    }
}