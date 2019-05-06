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
	public partial class AppointmentFeaturesPopupPage : PopupPage
    {
        #region Constructors
        public AppointmentFeaturesPopupPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void LoadFeatures(object sender, StateChangedEventArgs e)
        {

            var _button = (SfRadioButton)sender;

            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                _button.TextColor = Color.Black;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                _button.TextColor = Color.DimGray;
            }
            MainViewModel.GetInstance().NewAppointmentPopup.LoadFeatures(sender);
        }
        #endregion
    }
}