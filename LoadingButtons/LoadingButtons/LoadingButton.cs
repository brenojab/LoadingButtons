using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LoadingButtons
{
    public class LoadingButton : RelativeLayout
    {
        public LoadingButton()
        {
            Children.Add(new Button()
                         , Constraint.Constant(0), Constraint.Constant(0)
                         , Constraint.RelativeToParent((p) => { return p.Width; })
                         , Constraint.RelativeToParent((p) => { return p.Height; }));


            Children.Add(new ActivityIndicator { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, HeightRequest = 30, WidthRequest = 30 }
                         , Constraint.RelativeToParent((p) => { return p.Width / 2 - 15; })
                         , Constraint.RelativeToParent((p) => { return p.Height / 2 - 15; }));
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(LoadingButton), string.Empty, propertyChanged: OnTextChanged);
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(LoadingButton), false, propertyChanged: OnIsBusyChanged);
        public static readonly BindableProperty ButtonBgColorProperty = BindableProperty.Create(nameof(ButtonBgColor), typeof(Color), typeof(LoadingButton), Color.White, propertyChanged: OnButtonBgColorChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public Color ButtonBgColor
        {
            get { return (Color)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as LoadingButton;

            if (control == null)
            {
                return;
            }

            SetTextBasedOnBusy(control, control.IsBusy, newValue as string);
        }

        static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as LoadingButton;

            if (control == null)
            {
                return;
            }

            SetTextBasedOnBusy(control, (bool)newValue, control.Text);
        }

        static void SetTextBasedOnBusy(LoadingButton control, bool isBusy, string text)
        {
            var activityIndicator = GetActivityIndicator(control);
            var button = GetButton(control);

            if (activityIndicator == null || button == null)
            {
                return;
            }

            activityIndicator.IsVisible = activityIndicator.IsRunning = isBusy;
            button.Text = isBusy ? string.Empty : control.Text;
        }

        static void OnButtonBgColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as LoadingButton;

            if (control == null)
            {
                return;
            }

            var button = GetButton(control);

            if (button == null)
            {
                return;
            }

            button.BackgroundColor = (Color)newValue;
        }

        static ActivityIndicator GetActivityIndicator(LoadingButton control)
        {
            return control.Children[1] as ActivityIndicator;
        }

        static Button GetButton(LoadingButton control)
        {
            return control.Children[0] as Button;
        }
    }
}
