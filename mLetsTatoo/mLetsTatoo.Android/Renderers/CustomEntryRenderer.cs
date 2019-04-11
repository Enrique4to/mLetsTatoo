﻿using Android.Content;
using Android.Graphics.Drawables;
using mLetsTatoo.Controls;
using mLetsTatoo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(handler: typeof(CustomEntry), target: typeof(CustomEntryRenderer))]
namespace mLetsTatoo.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var view = (CustomEntry)Element;
                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
                if (view.IsCurvedCornersEnabled)
                {

                    var gradientDrawable = new GradientDrawable();
                    gradientDrawable.SetCornerRadius(60f);
                    gradientDrawable.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                    gradientDrawable.SetColor(view.BackgroundColor.ToAndroid());

                    Control.SetBackground(gradientDrawable);

                    Control.SetPadding(10, 15, 10, 7);
                }
            }
        }
    }
}