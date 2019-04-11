using Android.Content;
using Android.Graphics.Drawables;
using mLetsTatoo.Controls;
using mLetsTatoo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(handler: typeof(CustomLabel), target: typeof(CustomLabelRenderer))]
namespace mLetsTatoo.Droid.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var view = (CustomLabel)Element;
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