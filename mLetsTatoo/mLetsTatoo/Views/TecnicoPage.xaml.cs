namespace mLetsTatoo.Views
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    public partial class TecnicoPage : TabbedPage
	{
		public TecnicoPage ()
		{
            InitializeComponent ();
            this.BarBackgroundColor = Color.FromRgb(20, 20, 20);
            this.BarTextColor = Color.FromRgb(200, 200, 200);

            this.CurrentPage = this.ProfileStack;

            this.CurrentPageChanged += (object sender, EventArgs e) =>
            {

                var i = this.Children.IndexOf(this.CurrentPage);

                MainViewModel.GetInstance().UserHome.TipoBusqueda = "All";
                this.NewsFeedStack.Icon = "NewsFeedUns.png";
                this.ImagesStack.Icon = "ImagesUns.png";
                this.ProfileStack.Icon = "NoUserPicUns.png";

                switch (i)
                {
                    case 0:
                        this.NewsFeedStack.Icon = "NewsFeed.png";
                        break;
                    case 1:
                        this.ImagesStack.Icon = "Image.png";
                        break;
                    case 2:
                        this.ProfileStack.Icon = "NoUserPic.png";
                        break;
                }
            };

        }
    }
}