﻿namespace mLetsTatoo.Views
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
        #endregion
        public decimal Cost
        {
            get { return cost; }
            set
            {
                cost = value;
                OnPropertyChanged(nameof(Cost)); // Notify that there was a change on this property
            }
        }
        public decimal Advance
        {
            get { return advance; }
            set
            {
                advance = value;
                OnPropertyChanged(nameof(Advance)); // Notify that there was a change on this property
            }
        }

        public NewDatePage ()
		{
			InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent();
        }
        private void LoadFeatures(object sender, StateChangedEventArgs e)
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