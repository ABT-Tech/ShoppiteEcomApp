using System;
using DellyShopApp.Models;
using DellyShopApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrderPage
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId =Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        private bool _open = false;
        
        public MyOrderPage()
        {
            InitializeComponent();
            InittMyOrderPage();
        }
        private async void InittMyOrderPage()
        {
            BasketItems.ItemsSource = await DataService.GetMyOrderDetails(orgId, userId);//DataService.Instance.ProcutListModel;
        }

        private void OpenDetailClick(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stackLayout)) return;
            if ((stackLayout.BindingContext is ProductListModel pModel))
            {
                pModel.VisibleItemDelete = pModel.VisibleItemDelete == _open;
                pModel.Rotate = pModel.VisibleItemDelete == _open ? 0 : 90;
            }

        }

        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is StackLayout pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }
    }
}