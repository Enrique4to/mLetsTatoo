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
        }
        #endregion

        #region Methods
        #endregion
    }
}