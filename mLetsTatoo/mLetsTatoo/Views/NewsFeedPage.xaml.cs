using mLetsTatoo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mLetsTatoo.Views
{
	public partial class NewsFeedPage : ContentPage
	{
		public NewsFeedPage ()
		{
			InitializeComponent ();
		}
        private void NewsTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = true;
            Citas.IsVisible = false;
            Studios.IsVisible = false;
            Artists.IsVisible = false;
            Notificatios.IsVisible = false;
            Options.IsVisible = false;

            ImgNews.Source = "NewsFeed.png";
            ImgCitas.Source = "CitasUns.png";
            ImgLocal.Source = "LocalUns.png";
            ImgTattoo.Source = "TattooUns.png";
            ImgNotif.Source = "NotificacionUns.png";
            ImgOptions.Source = "OptionsUns.png";
            SearchBar.Placeholder = Languages.Search;
        }

        private void CitasTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = false;
            Citas.IsVisible = true;
            Studios.IsVisible = false;
            Artists.IsVisible = false;
            Notificatios.IsVisible = false;
            Options.IsVisible = false;

            ImgNews.Source = "NewsFeedUns.png";
            ImgCitas.Source = "Citas.png";
            ImgLocal.Source = "LocalUns.png";
            ImgTattoo.Source = "TattooUns.png";
            ImgNotif.Source = "NotificacionUns.png";
            ImgOptions.Source = "OptionsUns.png";
            SearchBar.Placeholder = Languages.Search;
        }
        private void StudiosTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = false;
            Citas.IsVisible = false;
            Studios.IsVisible = true;
            Artists.IsVisible = false;
            Notificatios.IsVisible = false;
            Options.IsVisible = false;

            ImgNews.Source = "NewsFeedUns.png";
            ImgCitas.Source = "CitasUns.png";
            ImgLocal.Source = "Local.png";
            ImgTattoo.Source = "TattooUns.png";
            ImgNotif.Source = "NotificacionUns.png";
            ImgOptions.Source = "OptionsUns.png";
            SearchBar.Placeholder = Languages.SearchStudios;
        }

        private void ArtistsTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = false;
            Citas.IsVisible = false;
            Studios.IsVisible = false;
            Artists.IsVisible = true;
            Notificatios.IsVisible = false;
            Options.IsVisible = false;

            ImgNews.Source = "NewsFeedUns.png";
            ImgCitas.Source = "CitasUns.png";
            ImgLocal.Source = "LocalUns.png";
            ImgTattoo.Source = "Tattoo.png";
            ImgNotif.Source = "NotificacionUns.png";
            ImgOptions.Source = "OptionsUns.png";
            SearchBar.Placeholder = Languages.SearchArtists;
        }
        private void NotifTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = false;
            Citas.IsVisible = false;
            Studios.IsVisible = false;
            Artists.IsVisible = false;
            Notificatios.IsVisible = true;
            Options.IsVisible = false;

            ImgNews.Source = "NewsFeedUns.png";
            ImgCitas.Source = "CitasUns.png";
            ImgLocal.Source = "LocalUns.png";
            ImgTattoo.Source = "TattooUns.png";
            ImgNotif.Source = "Notificacion.png";
            ImgOptions.Source = "OptionsUns.png";
            SearchBar.Placeholder = Languages.Search;
        }

        private void OptionsTab(object sender, EventArgs e)
        {
            NewsFeed.IsVisible = false;
            Citas.IsVisible = false;
            Studios.IsVisible = false;
            Artists.IsVisible = false;
            Notificatios.IsVisible = false;
            Options.IsVisible = true;

            ImgNews.Source = "NewsFeedUns.png";
            ImgCitas.Source = "CitasUns.png";
            ImgLocal.Source = "LocalUns.png";
            ImgTattoo.Source = "TattooUns.png";
            ImgNotif.Source = "NotificacionUns.png";
            ImgOptions.Source = "Options.png";
            SearchBar.Placeholder = Languages.Search;
        }
    }
}