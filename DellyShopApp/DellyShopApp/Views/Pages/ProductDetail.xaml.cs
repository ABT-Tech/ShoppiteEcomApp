using System;using System.Collections.Generic;using System.Drawing;
using System.Linq;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Color = System.Drawing.Color;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {        public int proId = Convert.ToInt32(SecureStorage.GetAsync("ProId").Result);        public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);
        public int UserId =Convert.ToInt32 (SecureStorage.GetAsync("UserId").Result);
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);


        int productCount = 1;        private static IEnumerable<ProductListModel> ItemsSource;        private readonly ProductListModel _products;
        private List<ProductListModel> _productLists = new List<ProductListModel>();
        public HomePage MainPage { get; }
        PancakeView lastCell;
      

        public ProductDetail(ProductListModel product)        {
            //if (product.ProductList.Length == 0 || (product.ProductList.Length == 1 && string.IsNullOrEmpty(product.ProductList[0])) )
            //{
            //    product.ProductList = new string[] { product.Image };
            //}
            _products = product;


          
            InitializeComponent();

            if ( _products.ProductList.Length != 0 )            {                specificationimage.IsVisible = false;                CarouselView.IsVisible = true;            }            else            {                specificationimage.IsVisible = true;                CarouselView.IsVisible = false;            }
            if (_products.Quantity <= 0)            {                ProductCountLabel.IsVisible = false;                Stocklbl.IsVisible = true;                Addtocartbtn.IsVisible = false;                BuyNowbtn.IsVisible = false;                plusimg.IsVisible = false;                minusimg.IsVisible = false;            }
            if (_products.WishlistedProduct == true)
            {
                myImage.Source = "red.png";
            }
            else
            {
                myImage.Source = "black.png";
            }
         
            this.BindingContext = product;
            MainPage = new HomePage();                    }
        protected override void OnAppearing()        {            InittProductDetail(_products);        }        private async void InittProductDetail(ProductListModel product)        {
           // Busy();
            List<ProductListModel> categories = new List<ProductListModel>();
            categories.Add(product);
            var similarpro = await DataService.GetSimilarProducts(orgId, Convert.ToInt32(product.CategoryId), Convert.ToInt32(product.BrandId));
            PreviousViewedList.ItemsSource = similarpro;
            if(similarpro.Count != 0)
            {
                similarlbl.IsVisible = true;
            }
            var atb = await DataService.GetProductVariation(orgId, product.ProductGUId);
            var atb2 = new List<Attributes>();
            //NotBusy();
            foreach(var Spect in atb)
            {
                if(Spect.IsSpecificationExist == true)
                {
                    atb2.Add(Spect);
                }   
            }
            foreach (var Sname in atb)            {                if (Sname.SpecificationNames == _products.SpecificationNames)                {                    Sname.BGColor = Color.Chocolate;                }                else                {                    Sname.BGColor = Color.Transparent;                }            }
            AttributeName.ItemsSource = atb2;

          
        }
        private void PlusClick(object sender, EventArgs e)        {
            {
                if (_products.Quantity >= 10)
                {
                    if (productCount <= 9)
                    {
                        ProductCountLabel.Text = (++productCount).ToString();
                    }
                }
                else
                {
                    if (productCount < _products.Quantity)
                    {
                        ProductCountLabel.Text = (++productCount).ToString();
                    }
                }
            }        }
             private void MinusClick(object sender, EventArgs e)        {            if (productCount <= 1) return;            ProductCountLabel.Text = (--productCount).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 20))            {                NavbarStack.IsVisible = false;                return;            }            NavbarStack.IsVisible = false;        }        private async void CommentsPageClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new CommentsPage(_products));        }

        private async void AddBasketButton(object sender, EventArgs e)        {            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Opps", "Please Login First!!", "Ok");
                await Navigation.PushAsync(new LoginPage());

            }            else
            {
                Cart cart = new Cart();
                cart.orgId = _products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                cart.SpecificationId = _products.SpecificationId;
                await DataService.AddToCart(cart);
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);
            }        }

        private async void BuyNow(object sender, EventArgs e)        {            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Opps", "Please Login First!!", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }            else
            {
                _productLists = new List<ProductListModel>();
                Cart cart = new Cart();
                cart.orgId = _products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);

                cart.SpecificationId = _products.SpecificationId;
                //await DataService.AddToCart(cart);
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await Navigation.PushAsync(new BasketPage(_productLists));
            }
        }
        private async void Imgtapp(System.Object sender, System.EventArgs e)
        {
            if (UserId == 0 || UserId == null)
            {
                await DisplayAlert("Opps", "Please Login First!!", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {

                var img = myImage.Source as FileImageSource;

                if (img.File == "red.png")
                {
                    myImage.Source = "black.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    favourite.SpecificationId = _products.SpecificationId;
                    await DataService.RemovefromFavourite(favourite.proId, favourite.UserId, favourite.orgId); //RemoveFavourite(favourite);
                }
                else
                {
                    myImage.Source = "red.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    favourite.SpecificationId = _products.SpecificationId;
                    await DataService.MyFavourite(favourite);
                }
            }
        }
       private async void ProductDetailClick(System.Object sender, System.EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));
        }
        private async void AttributeClick(object sender, EventArgs e)        {            if (lastCell != null)                lastCell.Content.BackgroundColor = Color.Transparent;            var pancake = (PancakeView)sender;            if (pancake. Content!= null)            {                pancake.Content.BackgroundColor = Color.Chocolate;                lastCell = pancake;
            }   
            if (!(sender is PancakeView stack)) return;            if (stack.Children.Count() <= 0) return;            if (!(stack.Children[0] is StackLayout sa)) return;            var specificationStack = sa.Children;            if (specificationStack.Count() <= 0) return;            if (!(specificationStack[1] is Label specLabel)) return;            var specificationValue = specLabel.Text;            Attributes attributeList = new Attributes();            attributeList.OrgId = _products.orgId;            attributeList.UserId = UserId;            attributeList.ProductGUId = _products.ProductGUId;            attributeList.SpecificationId = Convert.ToInt32(specificationValue);
            attributeList.DefaultSpecification = _products.DefaultSpecification;
            var item = await DataService.GetProductDetailsBySpecifcation(attributeList.OrgId, attributeList.ProductGUId, attributeList.SpecificationId,attributeList.UserId);            NavbarStack.BindingContext = item;
            item.SpecificationId= Convert.ToInt32(specificationValue);
            await Navigation.PushAsync(new ProductDetail(item));
        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
        }
        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;

        }
    }
}