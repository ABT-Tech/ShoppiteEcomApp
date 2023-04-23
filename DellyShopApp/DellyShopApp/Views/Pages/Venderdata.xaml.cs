using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Venderdata
    {
        protected override void OnAppearing()
        {
          
        }

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        //public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);
        public int userId =Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        private bool _open = false;
        private List<OrderListModel> orderId;

        public Venderdata()
        {
            InitializeComponent();
            InittMyOrderPage();
        }
        private async void InittMyOrderPage()
        {
           
            BasketItems.ItemsSource = await DataService.GetOrderDetails(orgId);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is VendorsOrder item)
            {
                await Navigation.PushAsync(new VendorsOrderDetails(item.orderId));
            }           
        }     
    }
}