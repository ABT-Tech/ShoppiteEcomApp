using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.TabbedPages;using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;using DellyShopApp.ViewModel;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class MyCartPage
    {
        protected override void OnAppearing()
        {
            InittMyCartPage();
        }
        List< ProductListModel> productListModel = new List<ProductListModel> ();
        private readonly ProductListModel _products;
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;

        int MyCartCountLable;

        public object Product { get; private set; }


        public MyCartPage()        {
           
            InitializeComponent();
            //if (_products.productQty == 0)
            //{
            //    //ProductCountLabel.IsVisible = false;
            //    //Stocklbl.IsVisible = true;
            //    //Addtocartbtn.IsVisible = false;
            //    //BuyNowbtn.IsVisible = false;
            //    //plusclick.IsVisible = false;
            //    //minus.IsVisible = false;
            //}
           
                if (userId == 0 || userAuth != "Client")
            {
                Login.IsVisible = true;
                checkout.IsVisible = false;
                txt.IsVisible = true;
                cartimg.IsVisible = true;
                vendorlogin.IsVisible = true;
                
            }
            else
            {
                Login.IsVisible = false;
                checkout.IsVisible = true;
                txt.IsVisible = false;
                cartimg.IsVisible = false;
                vendorlogin.IsVisible = false;
            }
 
            InittMyCartPage();
        }
       
        
        private async void InittMyCartPage()        {
            Busy();
            productListModel = await DataService.GetAllCartDetails(orgId, userId);
          
            foreach (var product in productListModel)            {                if (product.productQty == 0)                {                    product.IsOutStock = true;                    product.IsPriceVisible = false;                }                else
                {                    product.IsOutStock = false;                    product.IsPriceVisible = true;                }            }
            foreach (var varient in productListModel)            {                if (varient.SpecificationNames != "")                {                    varient.IsSpecificationNames = true;                }                else
                {
                    varient.IsSpecificationNames = false;                }            }
            NotBusy();
            BasketItems.ItemsSource = productListModel; //DataService.Instance.ProcutListModel;
            var productid = Convert.ToString(productListModel.Count);            if (productListModel.Count > 0 && (userId > 0 && userAuth == "Client"))            {                checkout.IsVisible = true;                gif.IsVisible = false;                shopping.IsVisible = false;            }            else if (productListModel.Count == 0 && userId > 0 && (userId > 0 && userAuth == "Client"))            {                checkout.IsVisible = false;                gif.IsVisible = true;                shopping.IsVisible = true;            }            else            {                checkout.IsVisible = false;                gif.IsVisible = false;                shopping.IsVisible = false;            }
        }
        public void Busy()        {            uploadIndicator.IsVisible = true;            uploadIndicator.IsRunning = true;

        }        public void NotBusy()        {            uploadIndicator.IsVisible = false;            uploadIndicator.IsRunning = false;

        }

        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;
            int Id = DataService.Instance.order.orgId;
            int UserId = DataService.Instance.order.UserId;
            await Navigation.PushAsync(new ProductDetail(item));        }        private async void Button_Clicked(object sender, EventArgs e)        {
            var prodItems = productListModel;            prodItems.RemoveAll(x => x.IsOutStock == true);            await Navigation.PushAsync(new BasketPage(prodItems));        }        private async void PlusClick(object sender, EventArgs e)        {
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);
            //if (CurrentQuantity >= 10) return;            //MyCartCountLable.Text = (++CurrentQuantity).ToString();            Label CartSelectedProduct = (Label)repaterStack.Children[2];
            var products = productListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = ++CurrentQuantity;
            if (products.productQty >= 10)            {                if (CurrentQuantity <= 10)                {                    MyCartCountLable.Text = (CurrentQuantity).ToString();                }            }            else            {                if (CurrentQuantity < products.productQty)                {                    MyCartCountLable.Text = (CurrentQuantity).ToString();                }            }
        }        private async void MinusClick(object sender, EventArgs e)        { 
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            Label CartSelectedProduct = (Label)repaterStack.Children[2];            if (CurrentQuantity == 1) return;
            MyCartCountLable.Text = (--CurrentQuantity).ToString();
            var products = productListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = CurrentQuantity;
         
        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {
                // NavbarStack.IsVisible = true;
                return;            }
            // NavbarStack.IsVisible = false;
        }

        protected void LogInClick(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

      private async  void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new VenderLoginPage());
        }

       private async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            {
                Image image = (Image)sender;
                if (!(image.Parent.Parent.Parent.Parent.Parent is PancakeView pancake)) return;
                if (!(pancake.BindingContext is ProductListModel item)) return;
                await DataService.RemoveFromCart(userId, orgId, item.Id,item.SpecificationIds);
                await DisplayAlert("Sucess !", "Item was Deleted", "Done");
                InittMyCartPage();
            }
        }
        private async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HomeTabbedPage());
        }

       private async void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HomeTabbedPage());
        }
    }}