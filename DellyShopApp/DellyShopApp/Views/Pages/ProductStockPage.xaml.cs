using System;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using PayPal.Forms.Abstractions;
using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductStockPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        private bool _open = false;        private ProductListModel _product;
               public ProductStockPage()        {
                        InitializeComponent();            InittMyOrderPage();        }        private async void InittMyOrderPage()        {            BasketItems.ItemsSource = await DataService.GetAllProductsByOrganizations(orgId); //await DataService.GetMyOrderDetails(orgId, userId);//
        }



        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)        {
            Frame frame = (Frame)sender;            if (!(frame.Parent.Parent is StackLayout pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            UpdateProductInfo updateProductInfo = new UpdateProductInfo();            updateProductInfo.orgId = orgId;            updateProductInfo.Price = item.Price;            updateProductInfo.Quantity = item.Quantity;            updateProductInfo.Id = item.Id;
            //item.Price = _products.Price;
            //item.Quantity = _products.Quantity;
            await DataService.UpdateProductDetail(updateProductInfo);            BasketItems.ItemsSource = await DataService.GetAllProductsByOrganizations(orgId);
        }        private async void PriceChange(object sender, EventArgs e)        {            Image imageSource = (Image)sender;            if (!(imageSource.Parent is StackLayout stack)) return;            stack.Children[0].IsEnabled = true;        }

        private async void QtyChange(object sender, EventArgs e)        {            Image imageSource = (Image)sender;            if (!(imageSource.Parent is StackLayout stack)) return;            stack.Children[0].IsEnabled = true;
                  }        private void OpenDetailClick(object sender, EventArgs e)        {            if (!(sender is StackLayout stackLayout)) return;            if ((stackLayout.BindingContext is ProductListModel pModel))            {                pModel.VisibleItemDelete = pModel.VisibleItemDelete == _open;                pModel.Rotate = pModel.VisibleItemDelete == _open ? 0 : 90;            }        }    }}