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
	public partial class AppointmentTimePopupPage : PopupPage
    {
        #region Constructors
        public AppointmentTimePopupPage()
        {
            InitializeComponent();
            if (MainViewModel.GetInstance().NewAppointmentPopup.changeDate == true)
            {
                this.Next.Source = "Check.png";
            }
            else
            {
                this.Next.Source = "Next.png";
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}