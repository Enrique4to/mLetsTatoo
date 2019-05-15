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
	public partial class AppointmentDescriptionPopupPage : PopupPage
    {
        #region Constructors
        public AppointmentDescriptionPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent();

            if (MainViewModel.GetInstance().NewAppointmentPopup.typeAppointment == true)
            {
                this.Next.Source = "Next.png";
            }
            else
            {
                this.Next.Source = "Check.png";
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}