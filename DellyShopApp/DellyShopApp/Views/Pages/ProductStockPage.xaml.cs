using System;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductStockPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private bool _open = false;

        public ProductStockPage()        {            InitializeComponent();            InittMyOrderPage();        }        private async void InittMyOrderPage()        {            BasketItems.ItemsSource = DataService.Instance.ProcutListModel; //await DataService.GetMyOrderDetails(orgId, userId);//
        }       
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Done", "Submited", "Ok");
        }

        private void PriceChange(object sender, EventArgs e)
        {
            Image imageSource = (Image)sender;
            if (!(imageSource.Parent is StackLayout stack)) return;
            stack.Children[0].IsEnabled = true;
        }
        private void OpenDetailClick(object sender, EventArgs e)        {            if (!(sender is StackLayout stackLayout)) return;            if ((stackLayout.BindingContext is ProductListModel pModel))            {                pModel.VisibleItemDelete = pModel.VisibleItemDelete == _open;                pModel.Rotate = pModel.VisibleItemDelete == _open ? 0 : 90;            }        }
        private void QtyChange(object sender, EventArgs e)
        {

            Image imageSource = (Image)sender;
            if(!(imageSource.Parent is StackLayout stack)) return;
            stack.Children[0].IsEnabled = true;
        }

    }}