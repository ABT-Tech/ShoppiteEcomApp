using System;using System.Collections.Generic;using System.Linq;using DellyShopApp.Helpers;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.TabbedPages;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ProductDetail    {        public int proId = Convert.ToInt32(SecureStorage.GetAsync("ProId").Result);                       int productCount;        private static IEnumerable<ProductListModel> ItemsSource;        private readonly List<StartList> _startList = new List<StartList>();        private readonly List<CommentModel> _comments = new List<CommentModel>();        private readonly ProductListModel _products;
       // private readonly Order _order1;



        public ProductDetail(ProductListModel product)        {
            //if (product.ProductList.Length == 0 || (product.ProductList.Length == 1 && string.IsNullOrEmpty(product.ProductList[0])) )
            //{
            //    product.ProductList = new string[] { product.Image };
            //}
            _products = product;            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _startList.Add(new StartList            {                StarImg = FontAwesomeIcons.Star            });            _comments.Add(new CommentModel            {                Name = "Ufuk Sahin",                CommentTime = "12/1/19",                Id = 1,                Rates = _startList            });            _comments.Add(new CommentModel            {                Name = "Hans Goldman",                CommentTime = "11/1/19",                Id = 2,                Rates = _startList.Skip(0).ToList()            });            InitializeComponent();            this.BindingContext = product;            starList.ItemsSource = _startList;            starListglobal.ItemsSource = _startList;            CommentList.ItemsSource = _comments;
            //MainScroll.Scrolled += MainScroll_Scrolled; 

            InittProductDetail();        }        private async void InittProductDetail()        {            ProductDetail.ItemsSource = DataService.Instance.ProcutListModel.Where(x => x.proId == proId);                   }        public ProductDetail(Order ca)        {        }

        public ProductDetail()
        {
        }

        private void PlusClick(object sender, EventArgs e)        {            if (productCount >= 10) return;            ProductCountLabel.Text = (++productCount).ToString();        }        private void MinusClick(object sender, EventArgs e)        {            if (productCount == 0) return;            ProductCountLabel.Text = (--productCount).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {                NavbarStack.IsVisible = true;                return;            }            NavbarStack.IsVisible = false;        }        private async void CommentsPageClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new CommentsPage(_products));        }

        async void AddBasketButton(object sender, EventArgs e)        {            Cart cart = new Cart();            cart.orgId = _products.orgId;            cart.UserId = 1;            cart.proId = _products.Id;            cart.Qty = Convert.ToInt32(ProductCountLabel.Text);            await DataService.AddToCart(cart);            await DisplayAlert(AppResources.Success, _products.Title + " " + AppResources.AddedBakset, AppResources.Okay);        }        private async void BuyNow(object sender, EventArgs e)        {            Cart cart = new Cart();
            cart.orgId = _products.orgId;            cart.UserId = 1;
            cart.proId = _products.Id;            cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
            await DataService.AddToCart(cart);
            //int Id = DataService.Instance.order.orgId;            //int UserId = DataService.Instance.order.UserId;
            await Navigation.PushAsync(new MyCartPage());                  }    }}