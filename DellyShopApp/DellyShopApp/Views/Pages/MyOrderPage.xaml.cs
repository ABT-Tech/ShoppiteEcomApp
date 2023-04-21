using System;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;
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
            InittMyOrderPage();
        }
        private async void InittMyOrderPage()
        {

            BasketItems.ItemsSource = await DataService.GetMyOrderDetails(orgId,userId);//DataService.Instance.vendors;



        }


        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is UserOrder item)
            {
                await Navigation.PushAsync(new UserOrderDetails(item.orderId));
            }

        }
    }
}