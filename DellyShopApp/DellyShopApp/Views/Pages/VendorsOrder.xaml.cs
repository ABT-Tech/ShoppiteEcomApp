using System;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.TabbedPages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VendorsOrder
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId =Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        private bool _open = false;
        
        public VendorsOrder()
        {
            InitializeComponent();
            InittMyOrderPage();
        }
        private async void InittMyOrderPage()
        {
            BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
            BasketItems.ItemsSource = DataService.Instance.vendors;           
        }

       

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new TabbedPages.VendorsOrderDetails());
        }

        //private async void ProductDetailClick(object sender, EventArgs e)
        //{
        //    if (!(sender is StackLayout pancake)) return;
        //    if (!(pancake.BindingContext is ProductListModel item)) return;
        //    await Navigation.PushAsync(new ProductDetail(item));
        //}
    }
}