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
            BasketItems.ItemsSource = await DataService.GetMyOrderDetails(orgId , userId);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is UserOrder item)
            {
                await Navigation.PushAsync(new UserOrderDetails(item.orderId));
            }
        }
    }
}