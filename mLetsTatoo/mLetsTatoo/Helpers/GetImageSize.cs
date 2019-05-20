using System;
using Xamarin.Forms;

#if __IOS__
using UIKit;
#endif

#if __ANDROID__
using Android.App;
using Android.Graphics;
using Android.Content.Res;
#endif
namespace mLetsTatoo.Helpers
{
    public static class ImageMeter
    {
        public static Size GetImageSize(string fileName)
        {
#if __IOS__
			UIImage image = UIImage.FromFile(fileName);
			return new Size((double)image.Size.Width, (double)image.Size.Height);
#endif

#if __ANDROID__
			var options = new BitmapFactory.Options {
				InJustDecodeBounds = true
			};
			fileName = fileName.Replace('-', '_').Replace(".png", "");
			var resId = Forms.Context.Resources.GetIdentifier(
				fileName, "drawable", Forms.Context.PackageName);
			BitmapFactory.DecodeResource(
				Forms.Context.Resources, resId, options);
			return new Size((double)options.OutWidth, (double)options.OutHeight);
#endif

            return Size.Zero;
        }
    }
}

