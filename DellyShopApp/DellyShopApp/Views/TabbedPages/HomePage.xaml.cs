using Acr.UserDialogs;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.Pages;using FFImageLoading.Forms;
using Plugin.Connectivity;
using SuaveControls.DynamicStackLayout;
using System;using System.Collections;using System.Collections.Generic;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomePage     {

        //Order product = new Order();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;        List<Category> categories = new List<Category>();        public List<Products> productDetails { get; set; }         private readonly ProductListModel _products;        private ProductListModel product;        public HomePage()        {            InitializeComponent();
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;
            //if (_products.WishlistedProduct == true)
            //{
            //    myImage.Source = "red.png";
            //}
            //else
            //{
            //    myImage.Source = "black.png";
            //}
        }        private async void InittHomePage()        {
            int? OrgUserID = UserId == 0 ? null : (int?)UserId;            categories = await DataService.GetAllCategories(orgId);            CategoryList.ItemsSource = DataService.Instance.CatoCategoriesList;
            CategoryList1.ItemsSource = DataService.Instance.CatoCategoriesList1;
            var images = DataService.Instance.BrandLogos;
            //MainCarouselView.ItemsSource = images;
            //Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>            //{            //    MainCarouselView.Position = (MainCarouselView.Position + 1) % images.Count;            //    return true;            //}));
            var carouselview1 = DataService.Instance.Carousel;
            CarouselView1.ItemsSource = carouselview1;
            Device.StartTimer(TimeSpan.FromSeconds(7), (Func<bool>)(() =>
            {                CarouselView1.Position = (CarouselView1.Position + 1) % carouselview1.Count;                return true;            }));

            var carouselview = categories.Where(x => x.Banner != null && x.Banner != "").ToList();
            CarouselView.ItemsSource = carouselview; //DataService.Instance.Carousel.Where(x => x.orgID == orgId);
            Device.StartTimer(TimeSpan.FromSeconds(7), (Func<bool>)(() =>
            {                CarouselView.Position = (CarouselView.Position + 1) % carouselview.Count;                return true;            }));
            BestSellerList.ItemsSource = await DataService.GetMostSellerProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            PreviousViewedList.ItemsSource = await DataService.GetLastVisitedProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); 
            MostNews.FlowItemsSource = await DataService.GetAllProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId).ToList();
            //var products = DataService.Instance.products;            //foreach (var pro in products)            //{            //    var label = new Label            //    {            //        Text = pro.Status,            //        Margin = 10,            //        Padding = 5,            //        FontAttributes = FontAttributes.Bold,            //        FontFamily = "{DynamicResource VerdanaProBold}",            //        FontSize = 18,            //        TextColor = Color.Black,            //        VerticalOptions = LayoutOptions.Start,            //        HorizontalOptions = LayoutOptions.StartAndExpand            //    };            //    var label2 = new Label
            //    {            //        Text = pro.all,            //        Margin = 10,            //        Padding = 5,            //        FontAttributes = FontAttributes.Bold,            //        FontFamily = "{DynamicResource VerdanaProBold}",            //        FontSize = 16,            //        TextColor = Color.Gray,            //        VerticalOptions = LayoutOptions.Start,            //        HorizontalOptions = LayoutOptions.End            //    };

            //    var stacklayout = new DynamicStackLayout();            //    stacklayout.Children.Add(label);            //    stacklayout.Children.Add(label2);            //    var stacklayout1 = new DynamicStackLayout();            //    stacklayout1.Orientation = StackOrientation.Horizontal;            //    foreach (var prolist in pro.ProductLists)            //    {            //        var cachedImage = new CachedImage            //        {            //            Source = prolist.Image,            //            Aspect = Aspect.AspectFill,            //            HeightRequest = 150,            //            Margin = 5            //        };
            //        var label3 = new Label            //        {            //            Text = prolist.Title,            //            FontFamily = "{ DynamicResource VerdanaProRegular }",            //            MaxLines = 1,            //            LineBreakMode = LineBreakMode.TailTruncation,            //            TextColor = Color.Black,            //            VerticalOptions = LayoutOptions.Start            //        };            //        var label4 = new Label            //        {            //            Text = prolist.Brand,            //            FontFamily = "{DynamicResource VerdanaProRegular}",            //            FontSize = 10,            //            HorizontalOptions = LayoutOptions.Start,            //            VerticalOptions = LayoutOptions.Start,            //            TextColor = Color.Black            //        };            //        var label5 = new Label            //        {            //            Text = prolist.OldPrice.ToString() + " ₹",            //            FontFamily = "{DynamicResource VerdanaProBold}",            //            FontSize = 18,            //            HorizontalOptions = LayoutOptions.EndAndExpand,            //            TextColor = Color.Gray,            //            WidthRequest = 80,            //            TextDecorations = TextDecorations.Strikethrough,            //            VerticalOptions = LayoutOptions.CenterAndExpand            //        };            //        var label6 = new Label            //        {            //            Text = prolist.Price.ToString() + " ₹",            //            FontFamily = "{DynamicResource VerdanaProBold}",            //            FontSize = 13,            //            HorizontalOptions = LayoutOptions.End,            //            TextColor = Color.Black,            //            WidthRequest = 80,            //            VerticalOptions = LayoutOptions.Center            //        };
            //        stacklayout1.Children.Add(label3);            //        stacklayout1.Children.Add(label4);            //        stacklayout1.Children.Add(label5);            //        stacklayout1.Children.Add(label6);
            //        stacklayout1.Children.Add(cachedImage);            //    }
                
              

            //    
            //    var scrollview = new ScrollView();
            //    var repeterview = new CustomControl.RepeaterView();
            //    repeterview.ItemTemplate = new DataTemplate();
            //    repeterview.ItemTemplate.
            //    repeterview.Children.Add(Image);
            //    repeterview.ItemsSource = pro.productListModel;
            //    HomeMainStack.Children.Add(stacklayout);
            //    HomeMainStack.Children.Add(repeterview);
            //    HomeMainStack.Children.Add(stacklayout1);                                         //}
        }
   
        protected override void OnAppearing()        {            if (ChechConnectivity())            {                InittHomePage();            }

        }
        private bool ChechConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");
                return false;
            }
        }

        private async void ProductDetailClick(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }        private async void ClickCategory(object sender, EventArgs e)        {            if (!(sender is StackLayout stack)) return;            if (!(stack.BindingContext is Category ca)) return;            await Navigation.PushAsync(new CategoryDetailPage(ca));        }        async void VireAllTapped(System.Object sender, System.EventArgs e)        {            await Navigation.PushAsync(new BestSellerPage());        }        void DragGestureRecognizer_DropCompleted(System.Object sender, Xamarin.Forms.DropCompletedEventArgs e)        {            BasketLayout.IsVisible = false;        }        async void DropBasketITem(System.Object sender, Xamarin.Forms.DropEventArgs e)        {            if (!DataService.Instance.BasketModel.Contains(product))                DataService.Instance.BasketModel.Add(product);            if (!DataService.Instance.BasketModel.Contains(product))                DataService.Instance.BasketModel.Add(product);            if (UserId == 0 || UserId == null || userAuth != "Client")            {                await DisplayAlert("Opps", "Please Login First!!", "Ok");                await Navigation.PushAsync(new LoginPage());            }            else            {                Cart cart = new Cart();                cart.orgId = product.orgId;                cart.UserId = Convert.ToInt32(UserId);                cart.proId = product.Id;                cart.Qty = Convert.ToInt32(ProductCountLabel.Text);                await DataService.AddToCart(cart);                await DisplayAlert(AppResources.Success, product.Title + " " + AppResources.AddedBakset, AppResources.Okay);                var productId = Convert.ToString(product.Id);
                //await Xamarin.Essentials.SecureStorage.SetAsync("ProId", productId);

            }        }        void DragGestureRecognizer_DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)        {            BasketLayout.IsVisible = true;            product = ((sender as Element).BindingContext) as ProductListModel;        }        private async void Click_Banner(System.Object sender, System.EventArgs e)        {            if (!(sender is ContentView content)) return;            if (!(content.BindingContext is Category c)) return;            Category Ca = categories.Where(x => x.Banner != null && x.Banner != "").FirstOrDefault();            await Navigation.PushAsync(new CategoryDetailPage(Ca));        }        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)        {            await Navigation.PushAsync(new MainPage());        }        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)        {            SearchBar searchBar = (SearchBar)sender;            if (searchBar.Text != "")            {                searchResults.IsVisible = true;                              searchResults.ItemsSource = await DataService.SearchProducts(orgId, searchBar.Text);                            }            else            {                searchResults.IsVisible = false;            }                    }        private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)        {            var type = sender.GetType();            var evnt = (ProductListModel)searchResults.SelectedItem;            await Navigation.PushAsync(new ProductDetail(evnt));        }

       private async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!(sender is ImageButton stack)) return;            if (!(stack.BindingContext is Category ca)) return;            await Navigation.PushAsync(new CategoryDetailPage(ca));
        }

        private async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            //if (UserId == "" || UserId == null)
            //{
            //    await DisplayAlert("Opps", "Please Login First!!", "Ok");
            //    await Navigation.PushAsync(new LoginPage());
            //}
            //else
            //{

            //    var img = myImage.Source as FileImageSource;

            //    if (img.File == "red.png")
            //    {
            //        myImage.Source = "black.png";
            //        Favourite favourite = new Favourite();
            //        favourite.orgId = _products.orgId;
            //        favourite.UserId = Convert.ToInt32(UserId);
            //        favourite.proId = _products.Id;
            //        await DataService.RemovefromFavourite(favourite.proId, favourite.UserId, favourite.orgId); //RemoveFavourite(favourite);

            //    }
            //    else
            //    {
            //        myImage.Source = "red.png";
            //        Favourite favourite = new Favourite();
            //        favourite.orgId = _products.orgId;
            //        favourite.UserId = Convert.ToInt32(UserId);
            //        favourite.proId = _products.Id;
            //        await DataService.MyFavourite(favourite);

            //    }

        //}
        }

       

       private async void TapGestureRecognizer_Tapped_4(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BestSellerPage());
        }

        private async void TapGestureRecognizer_Tapped_5(System.Object sender, System.EventArgs e)
        {
           await Navigation.PushAsync(new MainPage());
        }

       private async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Topdealspage());
        }
    }}