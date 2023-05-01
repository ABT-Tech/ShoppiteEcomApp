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
    public partial class Venderdata
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
       
        private bool _open = false;
        
    
        public Venderdata()
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
           
            BasketItems.ItemsSource = await DataService.GetOrderDetails(orgId);//DataService.Instance.vendors;



        }


        private async  void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is VendorsOrder item)
            {
                await Navigation.PushAsync(new VendorsOrderDetails(item.orderId));
            }
            
        }
    }
}