using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.CustomControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomLabel : ContentView
    {
        public CustomLabel()
        {
            InitializeComponent();
        }

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

        //Show the read more label if word length > 20
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomLabel)bindable;
            if (newValue != null)
            {
                control.lblReadMore.IsVisible = false;
                control.customLabel.Text = (string)newValue;
                if (control.customLabel.Text.Split().Length > 25)
                {
                    control.ShortTextVisible = true;
                    control.ReadMoreLabel = true;
                }
            }
        }
        #endregion

        public bool ReadMoreLabel { get; set; }
        private bool _shortTextVisible;
        public bool ShortTextVisible
        {
            get => _shortTextVisible;
            set { _shortTextVisible = value; ShortTextPropertyChanged(); }
        }

        //By Default show first 20 words.
        private void ShortTextPropertyChanged()
        {
            if (Text != null)
            {
                if (ShortTextVisible)
                {
                    customLabel.Text = string.Join(" ", Text.Split().Take(30));
                    //customLabel.Text = LineBreakMode.TailTruncation = 4;
                    lblReadMore.Text = "Read More";
                    lblReadMore.IsVisible = true;
                    lblReadMore.TextDecorations = TextDecorations.Underline;

                }
                else
                {
                    customLabel.Text = Text;
                    lblReadMore.Text = "Read Less";
                    lblReadMore.TextDecorations = TextDecorations.Underline;

                }
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ShortTextVisible = !ShortTextVisible;
        }
    }

}

