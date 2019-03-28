using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class EditUserPage : ContentPage
	{
		public EditUserPage ()
		{
			InitializeComponent ();
        }
        private void SwPass_Toggled(object sender, ToggledEventArgs e)
        {
            StackPass.IsEnabled = e.Value;
            if (e.Value==true)
            {
                StackPass.Opacity = 1;
            }
            else
            {
                StackPass.Opacity = .5;
            }
        }
        private void SwEmail_Toggled(object sender, ToggledEventArgs e)
        {
            StackEmail.IsEnabled = e.Value;
            if (e.Value == true)
            {
                StackEmail.Opacity = 1;
            }
            else
            {
                StackEmail.Opacity = .5;
            }
        }
        private void SwPersonal_Toggled(object sender, ToggledEventArgs e)
        {
            StackPersonal.IsEnabled = e.Value;
            if (e.Value == true)
            {

                StackPersonal.Opacity = 1;
            }
            else
            {
                StackPersonal.Opacity = .5;
            }
        }

    }
}