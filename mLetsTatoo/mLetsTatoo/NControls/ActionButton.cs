﻿namespace mLetsTatoo.Controls
{
    using System;
    using NControl.Abstractions;
    using NGraphics;
    using Xamarin.Forms;
    using System.Windows.Input;
    using System.Collections.Generic;
    using Color = Xamarin.Forms.Color;
    using TextAlignment = Xamarin.Forms.TextAlignment;

    /// <summary>
    /// Implements a floating action button with a font awesome label as
    /// the icon. An action button is part of Google Material Design and 
    /// therefore has a few attributes that should be set correctly.
    /// 
    /// http://www.google.com/design/spec/components/buttons.html
    /// 
    /// 
    /// </summary>
    public class ActionButton : NControlView
    {
        #region Protected Members

        /// <summary>
        /// The button shadow element.
        /// </summary>
        protected readonly NControlView ButtonShadowElement;

        /// <summary>
        /// The button element.
        /// </summary>
        protected readonly NControlView ButtonElement;

        /// <summary>
        /// The button icon label.
        /// </summary>
        protected readonly FontAwesomeLabel ButtonIconLabel;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NControl.Controls.ActionButton"/> class.
        /// </summary>
        public ActionButton()
        {
            var layout = new Grid { Padding = 0, ColumnSpacing = 0, RowSpacing = 0 };

            ButtonShadowElement = new NControlView
            {
                DrawingFunction = (canvas, rect) => {

                    // Draw shadow
                    rect.Inflate(new NGraphics.Size(-4));
                    rect.Y += 4;

                    Device.OnPlatform(

                        //iOS
                        () => canvas.DrawEllipse(rect, null, new NGraphics.RadialGradientBrush(
                            new NGraphics.Color(0, 0, 0, 200), NGraphics.Colors.Clear)),

                        // Android
                        () => canvas.DrawEllipse(rect, null, new NGraphics.RadialGradientBrush(
                            new NGraphics.Point(rect.Width / 2, (rect.Height / 2) + 2),
                            new NGraphics.Size(rect.Width, rect.Height),
                            new NGraphics.Color(0, 0, 0, 200), NGraphics.Colors.Clear)),

                        // WP
                        () => canvas.DrawEllipse(rect, null, new NGraphics.RadialGradientBrush(
                            new NGraphics.Color(0, 0, 0, 200), NGraphics.Colors.Clear)),

                        null
                    );
                },
            };

            ButtonElement = new NControlView
            {
                DrawingFunction = (canvas, rect) => {

                    // Draw button circle
                    rect.Inflate(new NGraphics.Size(-8));
                    canvas.DrawEllipse(rect, null, new NGraphics.SolidBrush(ButtonColor.ToNColor()));
                }
            };

            ButtonIconLabel = new FontAwesomeLabel
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
                Text = FontAwesomeLabel.FAPlus,
                FontSize = 14,
            };

            layout.Children.Add(ButtonShadowElement);
            layout.Children.Add(ButtonElement);
            layout.Children.Add(ButtonIconLabel);

            Content = layout;
        }

        #region Properties

        /// <summary>
        /// The command property.
        /// </summary>
        public static BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ActionButton), null,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.Command = (ICommand)newValue;
                });

        /// <summary>
        /// Gets or sets the color of the buton.
        /// </summary>
        /// <value>The color of the buton.</value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set
            {

                if (Command != null)
                    Command.CanExecuteChanged -= HandleCanExecuteChanged;

                SetValue(CommandProperty, value);

                if (Command != null)
                    Command.CanExecuteChanged += HandleCanExecuteChanged;

            }
        }

        /// <summary>
        /// The command parameter property.
        /// </summary>
        public static BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ActionButton), null,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.CommandParameter = newValue;
                });

        /// <summary>
        /// Gets or sets the color of the buton.
        /// </summary>
        /// <value>The color of the buton.</value>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// The button color property.
        /// </summary>
        public static BindableProperty ButtonColorProperty =
            BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(ActionButton), Color.Gray,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.ButtonColor = (Color)newValue;
                });

        /// <summary>
        /// Gets or sets the color of the buton.
        /// </summary>
        /// <value>The color of the buton.</value>
        public Color ButtonColor
        {
            get { return (Color)GetValue(ButtonColorProperty); }
            set
            {
                SetValue(ButtonColorProperty, value);
                ButtonElement.Invalidate();
            }
        }

        /// <summary>
        /// The button font family property.
        /// </summary>
        public static BindableProperty ButtonFontFamilyProperty =
            BindableProperty.Create(nameof(ButtonFontFamily), typeof(string), typeof(ActionButton), null,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.ButtonFontFamily = (string)newValue;
                });

        /// <summary>
        /// Gets or sets the color of the buton.
        /// </summary>
        /// <value>The color of the buton.</value>
        public string ButtonFontFamily
        {
            get { return (string)GetValue(ButtonFontFamilyProperty); }
            set
            {
                SetValue(ButtonFontFamilyProperty, value);
                ButtonIconLabel.FontFamily = value;
            }
        }

        /// <summary>
        /// The button icon property.
        /// </summary>
        public static BindableProperty ButtonIconProperty =
            BindableProperty.Create(nameof(ButtonIcon), typeof(string), typeof(ActionButton), FontAwesomeLabel.FAPlus,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.ButtonIcon = (string)newValue;
                });

        /// <summary>
        /// Gets or sets the button icon.
        /// </summary>
        /// <value>The button icon.</value>
        public string ButtonIcon
        {
            get { return (string)GetValue(ButtonIconProperty); }
            set
            {
                SetValue(ButtonIconProperty, value);
                ButtonIconLabel.Text = value;
            }
        }

        /// <summary>
        /// The button icon property.
        /// </summary>
        public static BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(ActionButton), true,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.HasShadow = (bool)newValue;
                });

        /// <summary>
        /// Gets or sets a value indicating whether this instance has shadow.
        /// </summary>
        /// <value><c>true</c> if this instance has shadow; otherwise, <c>false</c>.</value>
        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set
            {
                SetValue(HasShadowProperty, value);

                if (value)
                    ButtonShadowElement.FadeTo(1.0, 250);
                else
                    ButtonShadowElement.FadeTo(0.0, 250);
            }
        }

        /// <summary>
        /// The button icon color property.
        /// </summary>
        public static BindableProperty ButtonIconColorProperty =
            BindableProperty.Create(nameof(ButtonIconColor), typeof(Color), typeof(ActionButton), Color.White,
                BindingMode.TwoWay, null, (bindable, oldValue, newValue) => {
                    var ctrl = (ActionButton)bindable;
                    ctrl.ButtonIconColor = (Color)newValue;
                });

        /// <summary>
        /// Gets or sets the button icon color.
        /// </summary>
        /// <value>The button icon.</value>
        public Color ButtonIconColor
        {
            get { return (Color)GetValue(ButtonIconColorProperty); }
            set
            {
                SetValue(ButtonIconColorProperty, value);
                ButtonIconLabel.TextColor = value;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Handles the can execute changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void HandleCanExecuteChanged(object sender, EventArgs args)
        {
            IsEnabled = Command.CanExecute(CommandParameter);
        }
        #endregion

        #region Touch Events

        /// <summary>
        /// Toucheses the began.
        /// </summary>
        /// <param name="points">Points.</param>
        public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);

            if (!IsEnabled)
                return false;

            ButtonElement.ScaleTo(1.15, 140, Easing.CubicInOut);

            ButtonIconLabel.ScaleTo(1.2, 140, Easing.CubicInOut);

            if (HasShadow)
            {
                ButtonShadowElement.TranslateTo(0.0, 3, 140, Easing.CubicInOut);
                ButtonShadowElement.ScaleTo(1.2, 140, Easing.CubicInOut);
                ButtonShadowElement.FadeTo(0.44, 140, Easing.CubicInOut);
            }

            return true;
        }

        /// <summary>
        /// Toucheses the cancelled.
        /// </summary>
        /// <param name="points">Points.</param>
        public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);

            if (!IsEnabled)
                return false;

            ButtonElement.ScaleTo(1.0, 140, Easing.CubicInOut);
            ButtonIconLabel.ScaleTo(1.0, 140, Easing.CubicInOut);

            if (HasShadow)
            {
                ButtonShadowElement.TranslateTo(0.0, 0.0, 140, Easing.CubicInOut);
                ButtonShadowElement.ScaleTo(1.0, 140, Easing.CubicInOut);
                ButtonShadowElement.FadeTo(1.0, 140, Easing.CubicInOut);
            }

            return true;
        }

        /// <summary>
        /// Toucheses the ended.
        /// </summary>
        /// <param name="points">Points.</param>
        public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);

            if (!IsEnabled)
                return false;

            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);

            ButtonElement.ScaleTo(1.0, 140, Easing.CubicInOut);

            ButtonIconLabel.ScaleTo(1.0, 140, Easing.CubicInOut);

            if (HasShadow)
            {
                ButtonShadowElement.TranslateTo(0.0, 0.0, 140, Easing.CubicInOut);
                ButtonShadowElement.ScaleTo(1.0, 140, Easing.CubicInOut);
                ButtonShadowElement.FadeTo(1.0, 140, Easing.CubicInOut);
            }

            return true;
        }

        #endregion

        /// <summary>
        /// On Measure
        /// </summary>
        /// <param name="widthConstraint"></param>
        /// <param name="heightConstraint"></param>
        /// <returns></returns>
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var retVal = base.OnMeasure(widthConstraint, heightConstraint);

            if (retVal.Request.Width > retVal.Request.Height)
                retVal.Request = new Xamarin.Forms.Size(retVal.Request.Width, retVal.Request.Width);
            else if (retVal.Request.Height > retVal.Request.Width)
                retVal.Request = new Xamarin.Forms.Size(retVal.Request.Height, retVal.Request.Height);

            retVal.Minimum = retVal.Request;
            return retVal;
        }
    }
}
