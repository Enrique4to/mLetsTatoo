namespace mLetsTatoo.Views
{
    using ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TecnicoViewJobPage : TabbedPage
	{
        public TecnicoViewJobPage()
		{
			InitializeComponent ();
            this.CurrentPage = this.Citas;
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);
            this.CurrentPageChanged += (object sender, EventArgs e) =>
            {

                var i = this.Children.IndexOf(this.CurrentPage);

                MainViewModel.GetInstance().TecnicoHome.TipoBusqueda = "All";
                this.Balance.Icon = "BalanceUns.png";
                this.Citas.Icon = "CitasUns.png";

                switch (i)
                {
                    case 0:
                        this.Citas.Icon = "Citas.png";
                        break;
                    case 1:
                        this.Balance.Icon = "Balance.png";
                        break;
                }

            };
        }
	}
}