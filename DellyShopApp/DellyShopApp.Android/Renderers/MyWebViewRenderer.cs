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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyWebview), typeof(MyWebViewRenderer))]
namespace DellyShopApp.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
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
            if(e.Url == "https://mewanuts.shooppy.in/")
            {
               //Xamarin.Forms.Application.SetCurrentApplication(new App());
                //App.Current.MainPage = new MainPage();
                (Xamarin.Forms.Application.Current).MainPage = new HomeTabbedPage();

            }

            //await mywebview.EvaluateJavaScriptAsync("javascript: alert('"+e.Url.ToString()+"');");
        }
        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    RunOnUiThread(() => {
        //        Task startupWork = new Task(() => { SimulateStartup(); });
        //        startupWork.Start();
        //    });
        //}
        public virtual void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            
        }
    }
}