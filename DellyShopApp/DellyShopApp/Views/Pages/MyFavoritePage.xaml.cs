using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using System;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFavoritePage
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public MyFavoritePage()

        {
            InitializeComponent();
            InittFavoritePage();
           
        }
        private async void InittFavoritePage()
        {
            BasketItems.ItemsSource = await DataService.GetWishlistByUser(orgId,userId);//DataService.Instance.ProcutListModel;
        }
        private async void ClickItem(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }
    }
}