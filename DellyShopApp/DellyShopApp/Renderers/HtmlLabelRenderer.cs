using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using HtmlLabelApp.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace HtmlLabelApp.Droid
{
    public class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            //if we have a new forms element, we want to update text with font style (as specified in forms-pcl) on native control
            if (e.NewElement != null)
                UpdateTextOnControl();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //if there is change in text or font-style, trigger update to redraw control
            if (e.PropertyName == nameof(HtmlLabel.Html))
            {
                UpdateTextOnControl();
            }
        }

        void UpdateTextOnControl()
        {
            if (Control == null)
                return;

            if (Element is HtmlLabel formsElement)
            {
                var htmlAsString = formsElement.Html ?? string.Empty;      // used by WebView
                var htmlAsSpanned = Html.FromHtml(htmlAsString, FromHtmlOptions.ModeCompact); // used by TextView

                Control.TextFormatted = htmlAsSpanned;
            }
        }
    }
}