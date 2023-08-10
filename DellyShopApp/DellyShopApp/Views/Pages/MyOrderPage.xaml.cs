using System;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrderPage
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);

        private bool _open = false;


        public MyOrderPage()
        {
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittMyOrderPage();
            }
           
        }
        private bool ChechConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");
                return false;
            }
        }
        private async void InittMyOrderPage()
        {
            Busy();
            var orderproduct = await DataService.GetMyOrderDetails(orgId, userId);
            BasketItems.ItemsSource = orderproduct;
            if (orderproduct.Count == 0)
            {
                gif.IsVisible = true;
                shopping.IsVisible = true;
            }
            NotBusy();



        }


        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is UserOrder item)
            {
                await Navigation.PushAsync(new UserOrderDetails(item.orderId));
            }

        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;

        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;

        }

       private async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HomeTabbedPage());
        }
    }
}