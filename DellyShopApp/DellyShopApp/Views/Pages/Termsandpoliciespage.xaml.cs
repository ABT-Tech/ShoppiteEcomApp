using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Termsandpoliciespage
    {
        public object Element { get; private set; }
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;
        public Termsandpoliciespage()
        {

            InitializeComponent();
           
        }

      private void TermsandConditionsclick(System.Object sender, System.EventArgs e)
      {
            var url = "https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/terms";
            Device.OpenUri(new Uri(url));
            //Content = new StackLayout
            //{
            //    Children =
            //         {
            //            new MyWebview()
            //           {
            //                url="https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/terms",
            //                WidthRequest = 300,
            //                HeightRequest = 900,
            //                data = "userName=xxx"
            //           },
            //      },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
        }
      private async  void ContactUsclick(System.Object sender, System.EventArgs e)
      {
            var url = "https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/contact_us";
            Device.OpenUri(new Uri(url));
            //Content = new StackLayout
            //{
            //    Children =
            //         {
            //            new MyWebview()
            //           {
            //                url="https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/contact_us",
            //                WidthRequest = 300,
            //                HeightRequest = 900,
            //                data = "userName=xxx"
            //           },
            //      },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};

        }
      private void CancellationandRefundPolicyclick(System.Object sender, System.EventArgs e)
      {
            var url = "https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/refund";
            Device.OpenUri(new Uri(url));
            //Content = new StackLayout
            //{
            //    Children =
            //         {
            //            new MyWebview()
            //           {
            //                url="https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/refund",
            //                WidthRequest = 300,
            //                HeightRequest = 900,
            //                data = "userName=xxx"
            //           },
            //      },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
        }
      private void PrivacyPolicyclick(System.Object sender, System.EventArgs e)
      {
            var url = "https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/privacy";
            Device.OpenUri(new Uri(url));
            //Content = new StackLayout
            //{
            //    Children =
            //         {
            //            new MyWebview()
            //           {
            //                url="https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/privacy",
            //                WidthRequest = 300,
            //                HeightRequest = 900,
            //                data = "userName=xxx"
            //           },
            //      },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
        }
      private void ShippingandDeliveryPolicyclick(System.Object sender, System.EventArgs e)
      {
            var url = "https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/shipping";
            Device.OpenUri(new Uri(url));

            //Content = new StackLayout
            //{
            //    Children =
            //         {
            //            new MyWebview()
            //           {
            //                url="https://merchant.razorpay.com/policy/MbPpC7d1Iran0a/shipping",
            //                WidthRequest = 300,
            //                HeightRequest = 900,
            //                data = "userName=xxx"
            //           },
            //      },
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand
            //};
        }
      private async void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
      {
            await Navigation.PopAsync();
      }
    }
}