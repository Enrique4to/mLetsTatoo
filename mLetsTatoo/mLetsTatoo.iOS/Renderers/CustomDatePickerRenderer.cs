using System;
using CoreGraphics;
using mLetsTatoo.Controls;
using mLetsTatoo.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(handler: typeof(CustomDatePicker), target: typeof(CustomDatePickerRenderer))]
namespace mLetsTatoo.iOS.Renderers
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
                return;
            var element = e.NewElement as CustomDatePicker;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            Control.BorderStyle = UITextBorderStyle.RoundedRect;
            Control.Layer.BorderColor = UIColor.FromRGB(83, 63, 149).CGColor;
            Control.Layer.CornerRadius = 10;
            Control.Layer.BorderWidth = 1f;
            Control.AdjustsFontSizeToFitWidth = true;
            Control.TextColor = UIColor.FromRGB(83, 63, 149);

            Control.ShouldEndEditing += (textField) => {
                var seletedDate = (UITextField)textField;
                var text = seletedDate.Text;
                if (text == element.Placeholder)
                {
                    Control.Text = DateTime.Now.ToLongDateString();
                }
                return true;
            };
        }

        private void OnCanceled(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }

        private void OnDone(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
    }
}