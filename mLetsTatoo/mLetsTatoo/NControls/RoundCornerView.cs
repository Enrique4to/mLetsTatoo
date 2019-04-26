namespace mLetsTatoo.Controls
{
    using NControl.Abstractions;
    using Xamarin.Forms;

    /// <summary>
    /// Rounded border control.
    /// </summary>
	public class RoundCornerView : NControlView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoundCornerView"/> class.
        /// </summary>
        public RoundCornerView()
        {
            IsClippedToBounds = true;
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
        }

        #region Properties

        /// <summary>
        /// The border width property.
        /// </summary>
        public static BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(RoundCornerView), 0.0,
                propertyChanged: (bindable, oldValue, newValue) => {
                    var ctrl = (RoundCornerView)bindable;
                    ctrl.BorderWidth = (double)newValue;
                });

        /// <summary>
        /// Gets or sets the border width
        /// </summary>
        /// <value>The corner radius.</value>
        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set
            {
                SetValue(BorderWidthProperty, value);
                Invalidate();
            }
        }

        /// <summary>
        /// The border color property.
        /// </summary>
        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Xamarin.Forms.Color), typeof(RoundCornerView),
                Xamarin.Forms.Color.Transparent,
                propertyChanged: (bindable, oldValue, newValue) => {
                    var ctrl = (RoundCornerView)bindable;
                    ctrl.BorderColor = (Xamarin.Forms.Color)newValue;
                });

        /// <summary>
        /// Gets or sets the border width
        /// </summary>
        /// <value>The corner radius.</value>
        public Xamarin.Forms.Color BorderColor
        {
            get { return (Xamarin.Forms.Color)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
                Invalidate();
            }
        }

        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundCornerView), 4,
                propertyChanged: (bindable, oldValue, newValue) => {
                    var ctrl = (RoundCornerView)bindable;
                    ctrl.CornerRadius = (int)newValue;
                });

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set
            {
                SetValue(CornerRadiusProperty, value);
                Invalidate();
            }
        }

        #endregion

    }
}

