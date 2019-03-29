
namespace mLetsTatoo.Views
{
    using ViewModels;
    using Xamarin.Forms;
    public partial class TecnicoPage : ContentPage
	{
		public TecnicoPage ()
		{
            this.Title = MainViewModel.GetInstance().Tecnico.tecnico.Apodo;
            InitializeComponent ();
		}
	}
}