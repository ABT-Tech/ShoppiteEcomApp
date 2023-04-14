using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.TabbedPages;using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;using DellyShopApp.ViewModel;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class MyCartPage
    {
        protected override void OnAppearing()
        {
            InittMyCartPage();

        }
        List< ProductListModel> productListModel = new List<ProductListModel> ();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public int proId = Convert.ToInt32(SecureStorage.GetAsync("Count").Result);
        int MyCartCountLable;

        public object Product { get; private set; }

        public MyCartPage()        {
            
            InitializeComponent();
            if(userId == 0)
            {
                Login.IsVisible = true;
                cartimg.IsVisible = true;
                txt.IsVisible = true;
                checkout.IsVisible = false;
                vendorlogin.IsVisible = true;
            }
            else
            {
                Login.IsVisible = false;
                cartimg.IsVisible = false;
                checkout.IsVisible = true;
                txt.IsVisible = false;
                vendorlogin.IsVisible = false;
            }
           
            
            InittMyCartPage();
        }  
        //public async void CheckUserLogin()
        //{
        //    if (userId == 0 )
        //    {
        //        await DisplayAlert("Opps", "Please Login First!!", "Ok");
        //        await Navigation.PushAsync(new LoginPage());
        //    }
        //}
        private async void InittMyCartPage()        {
            productListModel =await DataService.GetAllCartDetails(orgId, userId);
            BasketItems.ItemsSource = productListModel; //DataService.Instance.ProcutListModel;
            var productid = Convert.ToString(productListModel.Count);
            await Xamarin.Essentials.SecureStorage.SetAsync("Count", productid);
             int proId = Convert.ToInt32(SecureStorage.GetAsync("Count").Result);
            if (proId > 0 && userId > 0)
            {
                checkout.IsVisible = true;
                gif.IsVisible = false;
            }
            else if (proId == 0 && userId >0)
            {
                checkout.IsVisible = false;
                gif.IsVisible = true;
            }
            else
            {
                checkout.IsVisible = false;
                gif.IsVisible = false;
            }
        }
        
         

private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;
            int Id = DataService.Instance.order.orgId;
            int UserId = DataService.Instance.order.UserId;
            await Navigation.PushAsync(new ProductDetail(item));        }        private async void Button_Clicked(object sender, EventArgs e)        {            await Navigation.PushAsync(new BasketPage(productListModel));        }        private async void PlusClick(object sender, EventArgs e)        {
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            if (CurrentQuantity >= 10) return;            MyCartCountLable.Text = (++CurrentQuantity).ToString();            Label CartSelectedProduct = (Label)repaterStack.Children[2];
            var products = productListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = CurrentQuantity;        
        }        private async void MinusClick(object sender, EventArgs e)        { 
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            Label CartSelectedProduct = (Label)repaterStack.Children[2];            if (CurrentQuantity == 1) return;
            MyCartCountLable.Text = (--CurrentQuantity).ToString();
            var products = productListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = CurrentQuantity;
         
        }        //private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        //{        //    var height = Math.Round(Application.Current.MainPage.Height);        //    var ycordinate = Math.Round(e.ScrollY);        //    if (ycordinate > (height / 3))        //    {
        //        // NavbarStack.IsVisible = true;
        //        return;        //    }
        //    // NavbarStack.IsVisible = false;
        //}        protected void LogInClick(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LoginPage());
        }        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VendorLoginPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var deleteitem = new DeleteItem()
            {
                userId = userId,
                orgId = orgId,
                proId = proId
            };
            await DataService.Delete(deleteitem);
            await DisplayAlert("Yahh !", "Item was Deleted", "Done");
        }
    }}