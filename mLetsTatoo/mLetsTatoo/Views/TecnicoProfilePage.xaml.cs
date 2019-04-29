using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class TecnicoProfilePage : ContentPage
	{
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //this.layout.Children.Add(this.abex, () => new Rectangle(((this.layout.Width / 4) * 3) - (56 / 2), (this.layout.Height / 2) - (200), 56, 250));
            InitializeComponent();
            
        }
    }
}