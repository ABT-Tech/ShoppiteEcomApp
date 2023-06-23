using System;using System.Collections.Generic;using System.Linq;using System.Windows.Input;
using DellyShopApp.Helpers;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.ModalPages;
using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;
namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("orgId").Result);

        int productCount = 1 ;        private static IEnumerable<ProductListModel> ItemsSource;
       
        private readonly List<StartList> _startList = new List<StartList>();        private readonly List<CommentModel> _comments = new List<CommentModel>();        private  List<ProductListModel> _productLists = new List<ProductListModel>();        private  ProductListModel _products;
        

        public ProductDetail(ProductListModel product)        {
            //if (product.ProductList.Length == 0 || (product.ProductList.Length == 1 && string.IsNullOrEmpty(product.ProductList[0])) )
            //{
            //    product.ProductList = new string[] { product.Image };
            //}
            _products = product;            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _comments.Add(new CommentModel            {                Name = "Ufuk Sahin",                CommentTime = "12/1/19",                Id = 1,                Rates = _startList            });            _comments.Add(new CommentModel            {                Name = "Hans Goldman",                CommentTime = "11/1/19",                Id = 2,                Rates = _startList.Skip(0).ToList()            });            InitializeComponent();
            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                Address.IsVisible = false;
            }
            if (_products.Quantity <= 0)
            {
                ProductCountLabel.IsVisible = false;
                Stocklbl.IsVisible = true;
                stock.IsVisible = false;
                FewStock.IsVisible = false;
                Addtocartbtn.IsVisible = false;
                BuyNowbtn.IsVisible = false;
                plusimg.IsVisible = false;
                minusimg.IsVisible = false;
            }
            else if (_products.Quantity <= 5)
            {
                FewStock.IsVisible = true;
                Stocklbl.IsVisible = false;
                stock.IsVisible = false;
            }
            else if(_products.Quantity > 5)
            {
                FewStock.IsVisible = false;
                Stocklbl.IsVisible = false;
                stock.IsVisible = true;
            }
           
            
            if (_products.WishlistedProduct == true)
            {
                myImage.Source = "Redheart.png";
            }
            else
            {
                myImage.Source = "Heart.png";
            }
            InittProductDetail();
            this.BindingContext = product;            //starList.ItemsSource = _startList;            //this.BindingContext = product.Quantity;                        //starListglobal.ItemsSource = _startList;            //CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled;           
                   }        private  void InittProductDetail()
        {
            PreviousViewedList.ItemsSource = DataService.Instance.ProcutListModel; //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); //

        }
        public ProductDetail()        {        }        private void PlusClick(object sender, EventArgs e)        {            if (_products.Quantity >= 10)
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
        }        private void MinusClick(object sender, EventArgs e)        {            if (productCount <= 1) return;            ProductCountLabel.Text = (--productCount).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {                NavbarStack.IsVisible = true;                return;            }            NavbarStack.IsVisible = false;        }        private async void CommentsPageClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new CommentsPage(_products));        }
        async void AddBasketButton(object sender, EventArgs e)        {
            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Login", "To Continue Shopping Please Sign in", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }            else
            {
                Cart cart = new Cart();
                cart.orgId = _products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                await DataService.AddToCart(cart);
                await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);
                var productId = Convert.ToString(_products.Id);
                gotocart.IsVisible = true;
                Addtocartbtn.IsVisible = false;
                //await Xamarin.Essentials.SecureStorage.SetAsync("ProId", productId);            
            }
        }
        private async void BuyNow(object sender, EventArgs e)        {            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Login", "To Continue Shopping Please Sign in", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }            else
            {
                _productLists = new List<ProductListModel>();
                Cart cart = new Cart();
                cart.orgId =_products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await Navigation.PushAsync(new BasketPage(_productLists));
            }
        }
        private async void Imgtapp(System.Object sender, System.EventArgs e)
        {
            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Login", "To Continue Shopping Please Sign in", "Ok");
            }
            else
            {
                var img = myImage.Source as FileImageSource;
               
                if (img.File == "RedHeart.png")
                {
                    myImage.Source = "Heart.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    await DataService.RemovefromFavourite(favourite.proId, favourite.UserId, favourite.orgId); //RemoveFavourite(favourite);
                }
               
                else
                {
                    myImage.Source = "RedHeart.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    await DataService.MyFavourite(favourite);
                }
            }
        }

        private void gotocart_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new MyCartPage());
        }

        private async void ProductDetailClick(object sender, EventArgs e)
        {

            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Share.RequestAsync(new ShareTextRequest
            {
                Subject = "Shooppy",
                Uri = "https://shooppy.in"
            });
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyCartPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            List<ChangeAddress> changeAddresses = new List<ChangeAddress>();
            Navigation.PushAsync(new AddNewAddress(changeAddresses));

        }
    }
}