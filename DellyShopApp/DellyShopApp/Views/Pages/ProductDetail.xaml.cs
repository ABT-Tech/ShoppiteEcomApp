using System;using System.Collections.Generic;using System.Linq;using System.Windows.Input;
using DellyShopApp.Helpers;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {        public int proId = Convert.ToInt32(SecureStorage.GetAsync("ProId").Result);        public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);


        int productCount;        private static IEnumerable<ProductListModel> ItemsSource;
        private Color _myColor;
        private int clickTotal;
        private readonly List<StartList> _startList = new List<StartList>();        private readonly List<CommentModel> _comments = new List<CommentModel>();        private readonly ProductListModel _products;



        private void RaisePropertyChanged()
        {
            throw new NotImplementedException();
        }

        public ProductDetail(ProductListModel product)        {
            //if (product.ProductList.Length == 0 || (product.ProductList.Length == 1 && string.IsNullOrEmpty(product.ProductList[0])) )
            //{
            //    product.ProductList = new string[] { product.Image };
            //}
            _products = product;            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _comments.Add(new CommentModel            {                Name = "Ufuk Sahin",                CommentTime = "12/1/19",                Id = 1,                Rates = _startList            });            _comments.Add(new CommentModel            {                Name = "Hans Goldman",                CommentTime = "11/1/19",                Id = 2,                Rates = _startList.Skip(0).ToList()            });            InitializeComponent();            this.BindingContext = product;            starList.ItemsSource = _startList;            starListglobal.ItemsSource = _startList;            CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled; 

            InittProductDetail();        }        private async void InittProductDetail()        {            ProductDetail.ItemsSource = DataService.Instance.ProcutListModel.Where(x => x.Id == proId);

        }

        public ProductDetail()        {        }        private void PlusClick(object sender, EventArgs e)        {            if (productCount >= 10) return;            ProductCountLabel.Text = (++productCount).ToString();        }        private void MinusClick(object sender, EventArgs e)        {            if (productCount == 0) return;            ProductCountLabel.Text = (--productCount).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {                NavbarStack.IsVisible = true;                return;            }            NavbarStack.IsVisible = false;        }        private async void CommentsPageClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new CommentsPage(_products));        }

        async void AddBasketButton(object sender, EventArgs e)        {            Cart cart = new Cart();            cart.orgId = _products.orgId;            cart.UserId = 1;            cart.proId = _products.Id;            cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
            await DataService.AddToCart(cart);            await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);        }

        private async void BuyNow(object sender, EventArgs e)        {            Cart cart = new Cart();            cart.orgId = _products.orgId;            cart.UserId = 1;            cart.proId = _products.Id;            cart.Qty = Convert.ToInt32(ProductCountLabel.Text);            await DataService.AddToCart(cart);

            await Navigation.PushAsync(new LoginPage());
        }
        private async void Imgtapp(System.Object sender, System.EventArgs e)
        {
            var img = myImage.Source as FileImageSource;

            if (img.File == "red.png")
            {
                myImage.Source = "black.png";
                Favourite favourite = new Favourite();
               
                await DataService.RemoveMyFavourite(favourite);


            }
            else
            {
                myImage.Source = "red.png";
                Favourite favourite = new Favourite();
                favourite.orgId = _products.orgId;
                favourite.UserId = 1;
                favourite.proId = _products.Id;
                favourite.Title = _products.Title;
                favourite.Brand = _products.Brand;
                favourite.Image = _products.Image;
                favourite.Price = _products.Price;
                await DataService.MyFavourite(favourite);

            }
           
        }

      
    }

}