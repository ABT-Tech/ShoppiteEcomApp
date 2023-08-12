using System;
using System.Collections;
using System.Collections.Generic;
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
        //public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        private bool _open = false;
        private List<OrderListModel> orderId;

        public MyOrderPage()
        {
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittMyOrderPage();
            }
        }
        protected override void OnAppearing()
        {
            InittMyOrderPage();
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
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            MainLayout.Opacity = 0.7;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            MainLayout.Opacity = 100;
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is UserOrder item)
            {
                await Navigation.PushAsync(new UserOrderDetails(item.orderId));
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomeTabbedPage());
        }
    }
}