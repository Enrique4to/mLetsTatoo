namespace mLetsTatoo.Views
{
    using mLetsTatoo.ViewModels;
    using System;
    using Xamarin.Forms;
    public partial class EmpresaPage : ContentPage
    {
        public EmpresaPage()
        {
            this.Title = MainViewModel.GetInstance().Empresa.empresa.Nombre;
            InitializeComponent();
        }
        private void Tab1Clicked(object sender, EventArgs e)
        {
            stkTab1.IsVisible = true;
            stkTab2.IsVisible = false;
            stkTab3.IsVisible = false;
            stkTab4.IsVisible = false;
        }
        private void Tab2Clicked(object sender, EventArgs e)
        {
            stkTab1.IsVisible = false;
            stkTab2.IsVisible = true;
            stkTab3.IsVisible = false;
            stkTab4.IsVisible = false;
        }
        private void Tab3Clicked(object sender, EventArgs e)
        {
            stkTab1.IsVisible = false;
            stkTab2.IsVisible = false;
            stkTab3.IsVisible = true;
            stkTab4.IsVisible = false;
        }
        private void Tab4Clicked(object sender, EventArgs e)
        {
            stkTab1.IsVisible = false;
            stkTab2.IsVisible = false;
            stkTab3.IsVisible = false;
            stkTab4.IsVisible = true;
        }
    }
}