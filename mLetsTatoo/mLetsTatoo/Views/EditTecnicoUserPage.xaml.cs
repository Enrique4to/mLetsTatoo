namespace mLetsTatoo.Views
{
    using Xamarin.Forms;
    public partial class EditTecnicoUserPage : ContentPage
	{
        public EditTecnicoUserPage ()
		{
			InitializeComponent ();
            this.EnabledButton();
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
            this.EnabledButton();
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
            this.EnabledButton();
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
            this.EnabledButton();
        }
        private void EnabledButton()
        {
            if(StackPersonal.IsEnabled == true || StackPass.IsEnabled == true || StackEmail.IsEnabled == true)
                this.SaveBtn.IsEnabled = true;
           
            else
                this.SaveBtn.IsEnabled = false;
        }

    }
}