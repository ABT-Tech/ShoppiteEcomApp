 using Acr.UserDialogs;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using Plugin.Connectivity;
using SuaveControls.DynamicStackLayout;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        //Order product = new Order();
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        List<Category> categories = new List<Category>();
        public List<Products> ProductDetails { get; set; }
        private ProductListModel product;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ChechConnectivity())
            {
                InittHomePage();
            }
            if (BindingContext == null)
            {
                BindingContext = new ProductListModel();
            }
            Loader();
        }
        public HomePage()
        {
            InitializeComponent();            
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;
           
        }
        private async void InittHomePage()
        {
           
            int? OrgUserID = UserId == 0 ? null : (int?)UserId;
            categories = await DataService.GetAllCategories(orgId);
            CategoryList.ItemsSource = DataService.Instance.CatoCategoriesList; //
            //var images = DataService.Instance.BrandLogos;
            //MainCarouselView.ItemsSource = images; //
            //Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            //{
            //    MainCarouselView.Position = (MainCarouselView.Position + 1) % images.Count;
            //    return true;
            //}));
            CategoryList1.ItemsSource = DataService.Instance.CatoCategoriesList2nd; //
            var SecBanner = DataService.Instance.Carousel;
            CarouselView1.ItemsSource = SecBanner;
            Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            {
                CarouselView1.Position = (CarouselView1.Position + 1) % SecBanner.Count;
                return true;
            }));
            var carouselview = categories.Where(x => x.Banner != null && x.Banner != "").ToList();
            CarouselView.ItemsSource = carouselview;//DataService.Instance.Carousel.Where(x => x.orgID == orgId); //
            Device.StartTimer(TimeSpan.FromSeconds(4), (Func<bool>)(() =>
            {
                CarouselView.Position = (CarouselView.Position + 1) % carouselview.Count;
                return true;
            }));
            BestSellerList.ItemsSource = await DataService.GetMostSellerProductsByOrganizations(orgId,OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            PreviousViewedList.ItemsSource = await DataService.GetLastVisitedProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); //
            MostNews.FlowItemsSource = await DataService.GetAllProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId).ToList(); //
            var products = DataService.Instance.products;

          
            //foreach (var pro in products)
            //{
            //    var label = new Label
            //    {
            //        Text = pro.Stutus,
            //        Margin = 10,
            //        Padding = 5,
            //        FontAttributes = FontAttributes.Bold,
            //        FontFamily = "{DynamicResource VerdanaProBold}",
            //        FontSize = 18,
            //        TextColor = Color.Black,
            //        VerticalOptions = LayoutOptions.Start,
            //        HorizontalOptions = LayoutOptions.StartAndExpand
            //    };
            //    var label2 = new Label
            //    {
            //        Text = pro.all,
            //        Margin = 10,
            //        Padding = 5,
            //        FontAttributes = FontAttributes.Bold,
            //        FontFamily = "{DynamicResource VerdanaProBold}",
            //        FontSize = 16,
            //        TextColor = Color.Gray,
            //        VerticalOptions = LayoutOptions.Start,
            //        HorizontalOptions = LayoutOptions.End
            //    };

            //    var stacklayout = new StackLayout();
            //    stacklayout.Children.Add(label);
            //    stacklayout.Children.Add(label2);
            //    var pancake = new PancakeView();

            //    var stacklayout1 = new StackLayout();
            //    foreach (var prolist in pro.productListModel)
            //    {
            //        var image = new Image
            //        {
            //            Source = prolist.Image,
            //            Aspect = Aspect.AspectFill,
            //            HeightRequest = 100,
            //            WidthRequest = 100,
            //            Margin = 5
            //        };
            //        var label3 = new Label
            //        {
            //            Text = prolist.Title,
            //            FontFamily = "{ DynamicResource VerdanaProRegular }",
            //            MaxLines = 1,
            //            LineBreakMode = LineBreakMode.TailTruncation,
            //            TextColor = Color.Black,
            //            VerticalOptions = LayoutOptions.End
            //        };
            //        var label4 = new Label
            //        {
            //            Text = prolist.Brand,
            //            FontFamily = "{DynamicResource VerdanaProRegular}",
            //            FontSize = 10,
            //            HorizontalOptions = LayoutOptions.Start,
            //            VerticalOptions = LayoutOptions.End,
            //            TextColor = Color.Black
            //        };
            //        var label5 = new Label
            //        {
            //            Text = prolist.OldPrice.ToString() + " ₹",
            //            FontFamily = "{DynamicResource VerdanaProBold}",
            //            FontSize = 18,
            //            HorizontalOptions = LayoutOptions.EndAndExpand,
            //            TextColor = Color.Gray,
            //            WidthRequest = 80,
            //            TextDecorations = TextDecorations.Strikethrough,
            //            VerticalOptions = LayoutOptions.End
            //        };
            //        var label6 = new Label
            //        {
            //            Text = prolist.Price.ToString() + " ₹",
            //            FontFamily = "{DynamicResource VerdanaProBold}",
            //            FontSize = 13,
            //            HorizontalOptions = LayoutOptions.End,
            //            TextColor = Color.Black,
            //            WidthRequest = 80,
            //            VerticalOptions = LayoutOptions.End
            //        };
            //        stacklayout1.Children.Add(image);
            //        stacklayout1.Children.Add(label3);
            //        stacklayout1.Children.Add(label4);
            //        stacklayout1.Children.Add(label5);
            //        stacklayout1.Children.Add(label6);

            //    }

            //    // ////HomeMainStack
            //    // var scrollview = new ScrollView();
            //    //var repeterview = new CustomControl.RepeaterView();
            //    //repeterview.ItemTemplate = new DataTemplate();
            //    // ////repeterview.ItemTemplate.
            //    // ////repeterview.Children.Add(Image);
            //    //repeterview.ItemsSource = pro.productListModel;
            //    HomeMainStack.Children.Add(stacklayout);
            //    stacklayout.Orientation = StackOrientation.Horizontal;
            //    //HomeMainStack.Children.Add(repeterview);
            //    HomeMainStack.Children.Add(stacklayout1);

            //}

            //var alldate = DataService.Instance.products; //.Where(x => x.orgId == orgId);
            //float rows = (float)alldate.Count / 2;
            //double rowcount = Math.Round(rows);
            //if (alldate.Count % 2 == 1)
            //{
            //    rowcount = rowcount + 1;
            //}
            //var productIndex = 0;
            //for (int rowIndex = 0; rowIndex < rowcount; rowIndex++)
            //{
            //    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
            //    {
            //        if (productIndex >= alldate.Count)
            //        {
            //            break;
            //        }
            //        var product = alldate[productIndex];
            //        productIndex += 1;
            //        var label = new Label
            //        {
            //            Text = product.Stutus,
            //            Margin = 10,
            //            Padding = 5,
            //            FontAttributes = FontAttributes.Bold,
            //            FontFamily = "{DynamicResource VerdanaProBold}",
            //            FontSize = 18,
            //            TextColor = Color.Black,
            //            VerticalOptions = LayoutOptions.Start,
            //            HorizontalOptions = LayoutOptions.StartAndExpand
            //        };
            //        var label1 = new Label
            //        {
            //            Text = product.all,                     
            //            Margin = 10,
            //            Padding = 5,
            //            FontAttributes = FontAttributes.Bold,
            //            FontFamily = "{DynamicResource VerdanaProBold}",
            //            FontSize = 16,
            //            TextColor = Color.Gray,
            //            VerticalOptions = LayoutOptions.Start,
            //            HorizontalOptions = LayoutOptions.End
            //        };
            //        //var Orglabel = new Label
            //        //{
            //        //    Text = product.OrgId.ToString(),
            //        //    IsVisible = false
            //        //};
            //        //image.GestureRecognizers.Add(new TapGestureRecognizer
            //        //{
            //        //    Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text, product.Image.ToString())),
            //        //});
            //        Home.Children.Add(label, columnIndex, rowIndex);
            //        Home.Children.Add(label1, columnIndex, rowIndex);
            //        var stacklayout = new StackLayout();
            //        foreach (var prolist in product.productListModel)
            //        {
            //            var image = new Image
            //            {
            //                Source = prolist.Image,
            //                Aspect =Aspect.AspectFill,
            //                HeightRequest = 25,
            //                WidthRequest = 25,
            //                Margin = 5
            //            };
            //            var label3 = new Label
            //            {
            //                Text = prolist.Title,
            //                FontFamily = "{ DynamicResource VerdanaProRegular }",
            //                MaxLines = 1,
            //                LineBreakMode = LineBreakMode.TailTruncation,
            //                TextColor = Color.Black,
            //                VerticalOptions = LayoutOptions.End
            //            };
            //            var label4 = new Label
            //            {
            //                Text = prolist.Brand,
            //                FontFamily = "{DynamicResource VerdanaProRegular}",
            //                FontSize = 10,
            //                HorizontalOptions = LayoutOptions.Start,
            //                VerticalOptions = LayoutOptions.End,
            //                TextColor = Color.Black
            //            };
            //            var label5 = new Label
            //            {
            //                Text = prolist.OldPrice.ToString() + " ₹",
            //                FontFamily = "{DynamicResource VerdanaProBold}",
            //                FontSize = 18,
            //                HorizontalOptions = LayoutOptions.EndAndExpand,
            //                TextColor = Color.Gray,
            //                WidthRequest = 80,
            //                TextDecorations = TextDecorations.Strikethrough,
            //                VerticalOptions = LayoutOptions.CenterAndExpand
            //            };
            //            var label6 = new Label
            //            {
            //                Text = prolist.Price.ToString() + " ₹",
            //                FontFamily = "{DynamicResource VerdanaProBold}",
            //                FontSize = 13,
            //                HorizontalOptions = LayoutOptions.End,
            //                TextColor = Color.Black,
            //                WidthRequest = 80,
            //                VerticalOptions = LayoutOptions.Center
            //            };

            //            Home.Children.Add(image, columnIndex, rowIndex);
            //            Home.Children.Add(label3, columnIndex, rowIndex);
            //            Home.Children.Add(label4, columnIndex, rowIndex);
            //            Home.Children.Add(label5, columnIndex, rowIndex);
            //            Home.Children.Add(label6, columnIndex, rowIndex);
            //        }
            //        //shop.Children.Add(Orglabel, columnIndex, rowIndex);
            //    }
            //}

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
        private async void ProductDetailClick(object sender, EventArgs e)
        {

            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            var t = Task.Run(() =>
            {
                // Do some work on a background thread, allowing the UI to remain responsive
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() => {
                    Navigation.PushAsync(new ProductDetail(item));
                });
            });
            //await Navigation.PushAsync(new ProductDetail(item));
          
        }

        private async void ClickCategory(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stack)) return;
            if (!(stack.BindingContext is Category ca)) return;
            await Navigation.PushAsync(new CategoryDetailPage(ca));
        }
        async void VireAllTapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BestSellerPage());
        }
        async void VireAllTapped1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TopDeals());
        }
        void DragGestureRecognizer_DropCompleted(System.Object sender, Xamarin.Forms.DropCompletedEventArgs e)
        {
            BasketLayout.IsVisible = false;
        }
        async void DropBasketITem(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {

            if (!DataService.Instance.BasketModel.Contains(product))
                DataService.Instance.BasketModel.Add(product);
            if (UserId == 0 || UserId == null || userAuth != "Client")
            {
                await DisplayAlert("Login", "To Continue Shopping Please Sign in", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                if (product.Quantity <= 0)
                {
                    await DisplayAlert("Opps", "Product Out Of Stock", "Ok");
                }
                else
                {
                    Cart cart = new Cart();
                    cart.orgId = product.orgId;
                    cart.UserId = Convert.ToInt32(UserId);
                    cart.proId = product.Id;
                    cart.Qty = Convert.ToInt32(ProductCountLabel.Text);
                    await DataService.AddToCart(cart);
                    await DisplayAlert(AppResources.Success, product.Title + " " + AppResources.AddedBakset, AppResources.Okay);
                    var productId = Convert.ToString(product.Id);
                    //await Xamarin.Essentials.SecureStorage.SetAsync("ProId", productId);
                }
            }
        }
        void DragGestureRecognizer_DragStarting(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {
            BasketLayout.IsVisible = true;
            product = ((sender as Element).BindingContext) as ProductListModel;
        }
        private async void Click_Banner(System.Object sender, System.EventArgs e)
        {
            if (!(sender is ContentView content)) return;
            if (!(content.BindingContext is Category c)) return;
            Category Ca = categories.Where(x => x.Banner != null && x.Banner != "").FirstOrDefault();
            await Navigation.PushAsync(new CategoryDetailPage(Ca));
        }

        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar.Text != "")
            {
                searchResults.IsVisible = true;
                searchResults.ItemsSource = await DataService.SearchProducts(orgId, searchBar.Text);
            }
            else
            {
                searchResults.IsVisible = false;
            }
        }
        private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var type = sender.GetType();
            var evnt = (ProductListModel)searchResults.SelectedItem;
            await Navigation.PushAsync(new ProductDetail(evnt));
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (!(sender is ImageButton stack)) return;
            if (!(stack.BindingContext is Category ca)) return;
            await Navigation.PushAsync(new CategoryDetailPage(ca));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
          
            Navigation.PushAsync(new MainPage());
        }
        public async void Loader()
        {
            ai.IsRunning = true;
            aiLayout.IsVisible = true;
            await Task.Delay(2000);
            aiLayout.IsVisible = false;
            ai.IsRunning = false;
        }
    }
}
