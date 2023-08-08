using System;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

using DellyShopApp;
using DellyShopApp.Droid;
using DellyShopApp.Views.Pages;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyWebview), typeof(MyWebViewRenderer))]
namespace DellyShopApp.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        public string Url = SecureStorage.GetAsync("PaymentUrl").Result;

        public MyWebViewRenderer(Context context) : base(context)
        {

        }
        protected async override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var mywebview = Element as MyWebview;

                var postData = Encoding.UTF8.GetBytes(mywebview.data);
                Control.PostUrl(mywebview.url, postData);
                mywebview.Navigating += YourWebView_Navigating;
            }
            //await DialogService.ShowError( "Erro ao abrir as peças do PJe!", "Voltar", null);
            //await NavigationService.NavigateToPrevious();
        }
        public async void YourWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var mywebview = Element as MyWebview;
            if(e.Url == Url)

            {
                (Xamarin.Forms.Application.Current).MainPage = new NavigationPage(new HomeTabbedPage());
                 Xamarin.Essentials.SecureStorage.Remove("PaymentUrl");
            }
            //await mywebview.EvaluateJavaScriptAsync("javascript: alert('"+e.Url.ToString()+"');");
        }
    }
}