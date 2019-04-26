namespace mLetsTatoo.Views
{
    using mLetsTatoo.Helpers;
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
        private Grid _grid;
        private void OnGridSelect(object s, EventArgs e)
        {
            if (_grid != null)
            {
                _grid.BackgroundColor = Color.Transparent;
            }

            var button = (Grid)s;
            button.BackgroundColor = Color.DimGray;
            _grid = button;
        }
    }
}