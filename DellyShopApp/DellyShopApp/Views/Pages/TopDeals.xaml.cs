using System;
using System.Collections.Generic;
using System.ComponentModel;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class TopDeals
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public TopDeals()
        {
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittBestSellerPage();
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
        private async void InittBestSellerPage()
        {
            BestSeller.FlowItemsSource = await DataService.GetLastVisitedProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
        }
        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }
        void BestSeller_Refreshing(System.Object sender, System.EventArgs e)
        {
            DataService.Instance.ProcutListModel.Insert(0, new ProductListModel
            {
                Title = AppResources.ProcutTitle3,
                Brand = AppResources.ProductBrand3,

                Id = 4,
                Image = "iphone",
                Price = 499,
                VisibleItemDelete = false,
                ProductList = new string[] { "ip8_1", "ip8_2" }
            });
            BestSeller.IsRefreshing = false;
        }
    }
}
