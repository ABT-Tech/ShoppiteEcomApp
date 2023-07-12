using System;using System.Collections.Generic;using System.Drawing;
using System.Linq;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Color = System.Drawing.Color;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {        public int proId = Convert.ToInt32(SecureStorage.GetAsync("ProId").Result);        public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);
        public string UserId = SecureStorage.GetAsync("UserId").Result;
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
           
            
            //_startList.Add(new StartList
            //{
            //    StarImg = FontAwesomeIcons.Star
            //});
            //_startList.Add(new StartList
            //{
            //    StarImg = FontAwesomeIcons.Star
            //});
            //_startList.Add(new StartList
            //{
            //    StarImg = FontAwesomeIcons.Star
            //});
            //_startList.Add(new StartList
            //{
            //    StarImg = FontAwesomeIcons.Star
            //});
            //_startList.Add(new StartList
            //{
            //    StarImg = FontAwesomeIcons.Star
            //});
            //_comments.Add(new CommentModel
            //{
            //    Name = "Ufuk Sahin",
            //    CommentTime = "12/1/19",
            //    Id = 1,
            //    Rates = _startList
            //});
            //_comments.Add(new CommentModel
            //{
            //    Name = "Hans Goldman",
            //    CommentTime = "11/1/19",
            //    Id = 2,
            //    Rates = _startList.Skip(0).ToList()
            //});
            InitializeComponent();

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
            //this.BindingContext = product.Quantity;
            MainPage = new HomePage();            //starList.ItemsSource = _startList;            //starListglobal.ItemsSource = _startList;            //CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled; 

            InittProductDetail(product);        }        private async void InittProductDetail(ProductListModel product)        {            
            List<ProductListModel> categories = new List<ProductListModel>();
            categories.Add(product);
            PreviousViewedList.ItemsSource = await DataService.GetSimilarProducts(orgId, Convert.ToInt32(product.CategoryId), Convert.ToInt32(product.BrandId));
            AttributeName.ItemsSource = await DataService.GetProductVariation(orgId,product.ProductGUId);
            
        }

        public ProductDetail()        {        }        private void PlusClick(object sender, EventArgs e)        {
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
            }        }        private void MinusClick(object sender, EventArgs e)        {            if (productCount <= 1) return;            ProductCountLabel.Text = (--productCount).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {                NavbarStack.IsVisible = true;                return;            }            NavbarStack.IsVisible = false;        }        private async void CommentsPageClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new CommentsPage(_products));        }

        private async void AddBasketButton(object sender, EventArgs e)        {            if (UserId == "" || UserId == null || userAuth != "Client")
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

                await DataService.AddToCart(cart);
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);
            }        }

        private async void BuyNow(object sender, EventArgs e)        {            if (UserId == "" || UserId == null || userAuth != "Client")
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
                //await DataService.AddToCart(cart);
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await Navigation.PushAsync(new BasketPage(_productLists));
            }
        }
        private async void Imgtapp(System.Object sender, System.EventArgs e)
        {
            if (UserId == "" || UserId == null)
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
                    await DataService.RemovefromFavourite(favourite.proId, favourite.UserId, favourite.orgId); //RemoveFavourite(favourite);

                }
                else
                {
                    myImage.Source = "red.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    await DataService.MyFavourite(favourite);

                }

            }

        }

       private async void ProductDetailClick(System.Object sender, System.EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));
        }
        private void AttributeClick(object sender, EventArgs e)        {            if (lastCell != null)                lastCell.Content.BackgroundColor = Color.Transparent;                      var pancake = (PancakeView)sender;            if (pancake. Content!= null)            {                pancake.Content.BackgroundColor = Color.Chocolate;                lastCell = pancake;                           }
                    }

    }
}