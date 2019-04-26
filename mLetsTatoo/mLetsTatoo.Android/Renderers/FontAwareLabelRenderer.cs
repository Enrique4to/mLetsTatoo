using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using mLetsTatoo.Controls;
using mLetsTatoo.Droid.Controls;
using Android.Graphics;
using System.IO;
using mLetsTatoo.Controls.Droid;
using Android.Content;

[assembly: ExportRenderer (typeof (Label), typeof (FontAwareLabelRenderer))]
namespace mLetsTatoo.Controls.Droid
{
	/// <summary>
	/// Custom font label renderer.
	/// </summary>
	public class FontAwareLabelRenderer: LabelRenderer
	{
        public FontAwareLabelRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement != null) 
				UpdateFont();
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
		
			if(e.PropertyName == Label.FontFamilyProperty.PropertyName ||
				e.PropertyName == Label.FontSizeProperty.PropertyName ||
				e.PropertyName == Label.FontAttributesProperty.PropertyName ||
				e.PropertyName == Label.TextProperty.PropertyName)
				UpdateFont();
		}

		private void UpdateFont()
		{
			var fontName = Element.FontFamily;
			if (string.IsNullOrWhiteSpace (fontName))
				return;

			fontName = fontName.ToLowerInvariant ();
			if(NControls.Typefaces.ContainsKey(fontName))
				Control.SetTypeface(NControls.Typefaces[fontName], TypefaceStyle.Normal);	
		}
	}
}

