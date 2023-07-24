using System;using System.Collections.Generic;using System.Linq;using System.Windows.Input;
using DellyShopApp.Helpers;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;
namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);

        int productCount = 1 ;        private static IEnumerable<ProductListModel> ItemsSource;
       
        private readonly List<StartList> _startList = new List<StartList>();        private readonly List<CommentModel> _comments = new List<CommentModel>();        private  List<ProductListModel> _productLists = new List<ProductListModel>();        private  List<AttributeListModel> _attributeListModels = new List<AttributeListModel>();        public  ProductListModel _products;
        private  AttributeListModel _atblist;

        private  Attributes _attributes;
        PancakeView lastCell;

        //public Color BGColor { get; private set; }

        public ProductDetail(ProductListModel product)        {
            //if (product.ProductList.Length == 0 || (product.ProductList.Length == 1 && string.IsNullOrEmpty(product.ProductList[0])) )
            //{
            //    product.ProductList = new string[] { product.Image };
            //}
            _products = product;           var SpcName = _products.SpecificationNames;            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _comments.Add(new CommentModel            {                Name = "Ufuk Sahin",                CommentTime = "12/1/19",                Id = 1,                Rates = _startList            });            _comments.Add(new CommentModel            {                Name = "Hans Goldman",                CommentTime = "11/1/19",                Id = 2,                Rates = _startList.Skip(0).ToList()            });            InitializeComponent();
           
            if (_products.Quantity <= 0)
            {
                ProductCountLabel.IsVisible = false;
                Stocklbl.IsVisible = true;
                Addtocartbtn.IsVisible = false;
                BuyNowbtn.IsVisible = false;
                plusimg.IsVisible = false;
                minusimg.IsVisible = false;
            }
            if (_products.WishlistedProduct == true)
            {
                myImage.Source = "red.png";
            }
            else
            {
                myImage.Source = "black.png";
            }
            if ( _products.ProductList.Length != 0)            {                specificationimage.IsVisible = false;                CarouselView.IsVisible = true;                         }
            else
            {
                specificationimage.IsVisible = true;                CarouselView.IsVisible = false;
            }
            BindingContext = this;
            InittProductDetail(product);
            this.BindingContext = product;            //starList.ItemsSource = _startList;            //this.BindingContext = product.Quantity;                         //starListglobal.ItemsSource = _startList;            //CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled;           
                   }

        public ProductDetail()
        {
        }

        public async void InittProductDetail(ProductListModel product)        {
                       List<ProductListModel> categories = new List<ProductListModel>();            categories.Add(product);
            var SimilarPro = await DataService.GetSimilarProducts(orgId, Convert.ToInt32(product.CategoryId), Convert.ToInt32(product.BrandId));
            Busy();
            PreviousViewedList.ItemsSource = SimilarPro;
            if(SimilarPro.Count != 0)
            {
                Similarlbl.IsVisible = true;
            }
            var atb = await DataService.GetProductVariation(orgId, product.ProductGUId);            var atb2 = new List<Attributes>();
            foreach (var Spect in atb)            {                if (Spect.IsSpecificationExist == true)                {                    atb2.Add(Spect);                }
            }
            foreach (var Sname in atb)            {                if (Sname.SpecificationNames == _products.SpecificationNames)                {
                    Sname.BGColor = Color.Chocolate;
                }                else
                {
                    Sname.BGColor = Color.Transparent;
                }            }             AttributeName.ItemsSource = atb2;            NotBusy();        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
        }        private void PlusClick(object sender, EventArgs e)        {            if (_products.Quantity >= 10)
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
                await DisplayAlert("Opps", "Please Login First!!", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }            else
            {
                Cart cart = new Cart();
                cart.orgId = _products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                cart.SpecificationId = _products.SpecificationIds;
                //if (!(sender is Frame stack)) return;
                //if (stack.Parent.BindingContext is ProductListModel item) return;

                //cart.SpecificationId = Convert.ToInt32(specificationValue);
                await DataService.AddToCart(cart);
                await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);
                var productId = Convert.ToString(_products.Id);
                //await Xamarin.Essentials.SecureStorage.SetAsync("ProId", productId);
             
            }
        }
        private async void BuyNow(object sender, EventArgs e)        {            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Opps !", "Please Login First", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }            else
            {
                _productLists = new List<ProductListModel>();
                Cart cart = new Cart();
                cart.orgId =_products.orgId;
                cart.UserId = Convert.ToInt32(UserId);
                cart.proId = _products.Id;
                cart.SpecificationId = _products.SpecificationIds;
                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                cart.SpecificationId = _products.SpecificationIds;
                _products.Quantity = Convert.ToInt32(ProductCountLabel.Text);
                _productLists.Add(_products);
                await Navigation.PushAsync(new BasketPage(_productLists));
            }
        }
        private async void Imgtapp(System.Object sender, System.EventArgs e)
        {
            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Opps !", "Please Login First", "Ok");
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
                    favourite.SpecificationId = _products.SpecificationIds;
                    await DataService.RemovefromFavourite(favourite.proId, favourite.UserId, favourite.orgId); //RemoveFavourite(favourite);
                }
               
                else
                {
                    myImage.Source = "red.png";
                    Favourite favourite = new Favourite();
                    favourite.orgId = _products.orgId;
                    favourite.UserId = Convert.ToInt32(UserId);
                    favourite.proId = _products.Id;
                    favourite.SpecificationId = _products.SpecificationIds;
                    await DataService.MyFavourite(favourite);
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));
        }
        
        private async void AttributeClick(object sender, EventArgs e)
        {
            if (lastCell != null)
                lastCell.Content.BackgroundColor = Color.Transparent;
            var pancakeView = (PancakeView)sender;
            if (pancakeView.Content != null)
            {
                pancakeView.Content.BackgroundColor = Color.Chocolate;
                lastCell = pancakeView;
            }

            //stack.Children[0].Children._list[1].Text
            if (!(sender is PancakeView stack)) return;
            if(stack.Children.Count() <= 0) return;
            if (!(stack.Children[0] is StackLayout sa)) return;
            var specificationStack = sa.Children;
            if (specificationStack.Count() <= 0) return;
            if (!( specificationStack[1] is Label specLabel)) return;
            var specificationValue = specLabel.Text;

            Attributes attributeList = new Attributes(); 
            attributeList.OrgId = _products.orgId;
            attributeList.ProductGUId = _products.ProductGUId;
            attributeList.DefaultSpecification = _products.DefaultSpecification;
            attributeList.SpecificationIds = Convert.ToInt32(specificationValue);
            var item = await DataService.GetProductDetailsBySpecifcation(attributeList.OrgId, attributeList.ProductGUId, attributeList.SpecificationIds);
            NavbarStack.BindingContext = item;
            item.SpecificationIds = Convert.ToInt32(specificationValue);
            await Navigation.PushAsync(new ProductDetail(item));
        }

        
    }
}