using Microsoft.Office.Interop.Word;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlankApp3.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LargeTextLabel
    {
      

        #region Bindable Property

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(TextProperty),
            returnType: typeof(string),
            declaringType: typeof(CustomLabel),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged
            );

        public string Text
        {
            get { return (string)base.GetValue(TextProperty); }
            set { base.SetValue(TextProperty, value); }
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomLabel)bindable;
            if (newValue != null)
            {
                control.customLabel.Text = (string)newValue;
                var ss = control.customLabel.Text.Split().Length;
                if (control.customLabel.Text.Split().Length >= 30)
                {
                    control.ShortTextVisible = true;
                    control.ReadMoreLabel = true;
                }
            }
        }

        #endregion Bindable Property

        public bool ReadMoreLabel { get; set; }
        private bool _shortTextVisible;

        public bool ShortTextVisible
        {
            get => _shortTextVisible;
            set { _shortTextVisible = value; ShortTextPropertyChanged(); }
        }

        private void ShortTextPropertyChanged()
        {
            if (Text != null && Text.Split().Length >= 30)
            {
                if (ShortTextVisible)
                {
                    if (customLabel != null && !string.IsNullOrWhiteSpace(customLabel.Text) && customLabel.Text.Split().Length< 100)
                    {
                        Debug.WriteLine("");
                    }

                    customLabel.Text = string.Join(" ", Text.Split().Take(30));
                    lblReadMore.Text = "See more";
                    lblReadMore.IsVisible = true;
                }
                else
                {
                    customLabel.Text = Text;
                    lblReadMore.Text = "See less";
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShortTextVisible = !ShortTextVisible;
        }
    }
}