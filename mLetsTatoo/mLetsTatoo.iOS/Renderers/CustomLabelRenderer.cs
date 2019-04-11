using System;
using CoreGraphics;
using mLetsTatoo.Controls;
using mLetsTatoo.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(handler: typeof(CustomLabel), target: typeof(CustomLabelRenderer))]
namespace mLetsTatoo.iOS.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var view = (CustomLabel)Element;
                
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;
            }
        }
    }
}