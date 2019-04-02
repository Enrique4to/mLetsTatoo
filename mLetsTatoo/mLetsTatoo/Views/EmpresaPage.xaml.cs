namespace mLetsTatoo.Views
{
    using mLetsTatoo.ViewModels;
    using System;
    using Xamarin.Forms;
    public partial class EmpresaPage : TabbedPage
    {
        public EmpresaPage()
        {
            this.Title = MainViewModel.GetInstance().Empresa.empresa.Nombre;
            InitializeComponent();

            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
        }
    }
}