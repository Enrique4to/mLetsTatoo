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
	public partial class CancelDatePopupPage : PopupPage
    {
        #region Constructors
        public CancelDatePopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
            if(MainViewModel.GetInstance().UserViewDate.cancelPage == "CancelDate")
            {
                this.CancelMessage.Text = Languages.CancelUserDateMessage;
            }
            else if(MainViewModel.GetInstance().UserViewDate.cancelPage == "CancelTempDate")
            {
                this.CancelMessage.Text = Languages.CancelDateMessage;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}