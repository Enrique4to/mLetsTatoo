

namespace mLetsTatoo.Popups.Views
{
    using mLetsTatoo.Helpers;
    using mLetsTatoo.ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Rg.Plugins.Popup.Pages;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTecnicoMenuPopupPage : PopupPage
	{
        #region Attributes
        private int time;
        private string stringtime;
        private MediaFile file;
        private int ECostAddedComision;
        private int EAdvanceAddedComision;
        private int MCostAddedComision;
        private int MAdvanceAddedComision;
        private int HCostAddedComision;
        private int HAdvanceAddedComision;
        #endregion

        public EditTecnicoMenuPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
		}

        #region Methods

        #endregion
    }
}