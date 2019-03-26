using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using mLetsTatoo.Controls;
using mLetsTatoo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(handler: typeof(CustomEntry), target: typeof(CustomEntryRenderer))]
namespace mLetsTatoo.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomEntryRenderer : EntryRenderer
    {
        //public CustomEntryRenderer(Context context) : base(context)
        //{

        //}
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);
                }

                Control.Gravity = GravityFlags.CenterVertical;
                Control.Gravity = GravityFlags.CenterHorizontal;
                // Set padding for the internal text from border

                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(15)),
                    Control.PaddingRight,
                    (int)DpToPixels(this.Context, Convert.ToSingle(15)),
                    Control.PaddingTop);
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}