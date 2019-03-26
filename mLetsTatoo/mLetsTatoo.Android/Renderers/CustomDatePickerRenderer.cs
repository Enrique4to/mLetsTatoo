using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using mLetsTatoo.Controls;
using mLetsTatoo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(handler: typeof(CustomDatePicker), target: typeof(CustomDatePickerRenderer))]
namespace mLetsTatoo.Droid.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (CustomDatePicker)Element;

                GradientDrawable gd = new GradientDrawable();
                gd.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                gd.SetColor(view.BackgroundColor.ToAndroid());
                gd.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                this.Control.SetTextColor(view.TextColor.ToAndroid());
                this.Control.SetTextColor(view.PlaceholderColor.ToAndroid());
                this.Control.SetBackgroundColor(view.BackgroundColor.ToAndroid());
                this.Control.SetBackground(gd);                              

                CustomDatePicker element = Element as CustomDatePicker;

                if (!string.IsNullOrWhiteSpace(element.Placeholder))
                {
                    Control.Text = element.Placeholder;
                }

                this.Control.TextChanged += (sender, arg) =>
                {
                    var selectedDate = arg.Text.ToString();
                    if (selectedDate == element.Placeholder)
                    {
                        this.Control.Text = DateTime.Now.ToString("{0:d MMM yyyy}, dt");
                    }
                };
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}