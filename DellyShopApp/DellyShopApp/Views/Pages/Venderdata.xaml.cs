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
    public partial class Venderdata
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        private bool _open = false;

        public Venderdata()
        {
            InitializeComponent();
            InittMyOrderPage();
        }
        private async void InittMyOrderPage()
        {
            BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
            BasketItems.ItemsSource = DataService.Instance.vendors;



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

        //private async void ProductDetailClick(object sender, EventArgs e)
        //{
        //    if (!(sender is StackLayout pancake)) return;
        //    if (!(pancake.BindingContext is ProductListModel item)) return;
        //    await Navigation.PushAsync(new ve());
        //}

        private async  void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new VendorsOrderDetails());
        }
    }
}