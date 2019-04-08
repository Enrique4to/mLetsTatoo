using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class NewDatePage : ContentPage
	{
		public NewDatePage ()
		{
			InitializeComponent ();
            this.SmallChecked.IsChecked = true;
            this.EasyChecked.IsChecked = true;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent();
        }
        private void LoadCharacteristics(object sender, StateChangedEventArgs e)
        {
            if (SmallChecked.IsChecked == true)
            {
                Application.Current.MainPage.DisplayAlert("error", SmallChecked.IsChecked.Value.ToString(), "ok");
                //MediumSizeChecked.IsChecked = false;
                //BigChecked.IsChecked = false;

                //if (EasyChecked.IsChecked == true)
                //{
                //    MediumSizeChecked.IsChecked = false;
                //    BigChecked.IsChecked = false;
                //}
                //else if (MediumSizeChecked.IsChecked == true)
                //{
                //    EasyChecked.IsChecked = false;
                //    BigChecked.IsChecked = false;
                //}
                //else if (BigChecked.IsChecked.HasValue == true)
                //{
                //    MediumSizeChecked.IsChecked = false;
                //    EasyChecked.IsChecked = false;
                //}
            }
            //else if (MediumSizeChecked.IsChecked == true)
            //{

            //    SmallChecked.IsChecked = false;
            //    BigChecked.IsChecked = false;

            //    if (EasyChecked.IsChecked == true)
            //    {
            //        MediumSizeChecked.IsChecked = false;
            //        BigChecked.IsChecked = false;
            //    }
            //    else if (MediumSizeChecked.IsChecked == true)
            //    {
            //        EasyChecked.IsChecked = false;
            //        BigChecked.IsChecked = false;
            //    }
            //    else if (BigChecked.IsChecked == true)
            //    {
            //        MediumSizeChecked.IsChecked = false;
            //        EasyChecked.IsChecked = false;
            //    }
            //}
            //else if (BigChecked.IsChecked == true)
            //{

            //    MediumSizeChecked.IsChecked = false;
            //    SmallChecked.IsChecked = false;

            //    if (EasyChecked.IsChecked == true)
            //    {
            //        MediumSizeChecked.IsChecked = false;
            //        BigChecked.IsChecked = false;
            //    }
            //    else if (MediumSizeChecked.IsChecked == true)
            //    {
            //        EasyChecked.IsChecked = false;
            //        BigChecked.IsChecked = false;
            //    }
            //    else if (BigChecked.IsChecked == true)
            //    {
            //        MediumSizeChecked.IsChecked = false;
            //        EasyChecked.IsChecked = false;
            //    }
            //}
        }

    }
}