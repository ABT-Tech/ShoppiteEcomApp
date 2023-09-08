     using Acr.UserDialogs;
using DellyShopApp.CustomControl;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using FFImageLoading.Forms;
using Plugin.Connectivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {

        //Order product = new Order();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int OrgCatId = Convert.ToInt32(SecureStorage.GetAsync("OrgCatId").Result);
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;
        List<Category> categories = new List<Category>();

        private ProductListModel product;

        public HomePage()
        {
            InitializeComponent();
            
         //   ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;            
        }
        private async void InittHomePage()
        {
            //Busy();
            int? OrgUserID = UserId == 0 ? null : (int?)UserId;
            categories = await DataService.GetAllCategories(orgId);
            var AllProducts = await DataService.GetAllProductsByOrganizations(0, OrgUserID, OrgCatId);
            foreach(var prodDetails in AllProducts.MainProductDTOs)
            {
                BindStack(prodDetails.Status, prodDetails.productsDTOs);
            }
        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            MainLayout.Opacity = 0.7;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            MainLayout.Opacity = 100;
        }
        protected override void OnAppearing()
        {
            if (ChechConnectivity())
            {
                InittHomePage();
            }         

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
            await Navigation.PushAsync(new ProductDetail(item));
            //if(item.Quantity  >10 )
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
                    cart.SpecificationNames = product.SpecificationNames;
                    cart.SpecificationId = product.SpecificationId;
                    await DataService.AddToCart(cart);
                    await DisplayAlert(AppResources.Success, product.Title + " " + AppResources.AddedBakset, AppResources.Okay);
                    //var productId = Convert.ToString(product.Id);
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

       
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TopDeals());
        }

        public void BindStack(string label, List<ProductResponse> productResponses)
        {
            var dynamicResourceBold = new Xamarin.Forms.Internals.DynamicResource("VerdanaProBold");
            var dynamicResourceRegular = new Xamarin.Forms.Internals.DynamicResource("VerdanaProRegular");
            StackLayout stackLayout = new StackLayout();
            stackLayout.Orientation = StackOrientation.Horizontal;
            Label stacklabel = new Label();
            stacklabel.Margin = new Thickness(0, 10, 0, 0);
            stacklabel.Padding = new Thickness(5, 5, 0, 0);
            stacklabel.FontAttributes = FontAttributes.Bold;
            stacklabel.FontFamily = dynamicResourceBold.Key;
            stacklabel.FontSize = 22;
            stacklabel.HorizontalOptions = LayoutOptions.StartAndExpand;
            stacklabel.Text = label;
            stacklabel.TextColor = Color.Black;
            stacklabel.VerticalOptions = LayoutOptions.Start;
            stackLayout.Children.Add(stacklabel);
            ScrollView statusScrollView = new ScrollView();
            statusScrollView.HorizontalScrollBarVisibility = ScrollBarVisibility.Never;
            statusScrollView.Orientation = ScrollOrientation.Horizontal;
            RepeaterView statusRepeaterView = new RepeaterView();
            statusRepeaterView.Orientation = StackOrientation.Horizontal;
            statusRepeaterView.Spacing = -5;
            var dataTemplate = new DataTemplate(() => {
                ViewCell vc = new ViewCell();
                Image cacheImage = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HeightRequest = 150,
                    Margin = 5,
                    VerticalOptions = LayoutOptions.Start
                };
                cacheImage.SetBinding(Image.SourceProperty, "Image");
                Label stacklabelChild = new Label
                {
                    FontFamily = dynamicResourceRegular.Key,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Start
                };
                stacklabelChild.SetBinding(Label.TextProperty, "Title");
                
                
                
                Label childLabel = new Label
                {
                    FontFamily = dynamicResourceBold.Key,
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.Gray,
                    WidthRequest = 80,
                    TextDecorations = TextDecorations.Strikethrough,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                childLabel.SetBinding(Label.TextProperty, "OldPrice");
                Label childLabel2 = new Label
                {
                    FontFamily = dynamicResourceBold.Key,
                    FontSize = 13,
                    HorizontalOptions = LayoutOptions.End,
                    TextColor = Color.Black,
                    WidthRequest = 80,
                    VerticalOptions = LayoutOptions.Center
                };
                childLabel2.SetBinding(Label.TextProperty, "Price");

                StackLayout childStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.EndAndExpand
                };
                childStackLayout.Children.Add(childLabel);
                childStackLayout.Children.Add(childLabel2);

                StackLayout pancakeChildLayout = new StackLayout();
                pancakeChildLayout.Children.Add(cacheImage);
                pancakeChildLayout.Children.Add(stacklabelChild);
                pancakeChildLayout.Children.Add(childStackLayout);

                PancakeView dataTemplatePancakeView = new PancakeView();
                dataTemplatePancakeView.Margin = 5;
                dataTemplatePancakeView.Padding = 5;
                dataTemplatePancakeView.BackgroundColor = Color.White;
                dataTemplatePancakeView.CornerRadius = 8;
                dataTemplatePancakeView.Elevation = 3;
                dataTemplatePancakeView.HasShadow = true;
                dataTemplatePancakeView.HeightRequest = 250;
                dataTemplatePancakeView.HorizontalOptions = LayoutOptions.StartAndExpand;
                dataTemplatePancakeView.VerticalOptions = LayoutOptions.StartAndExpand;
                dataTemplatePancakeView.WidthRequest = 140;
                dataTemplatePancakeView.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => ProductDetailClick(dataTemplatePancakeView, null)),
                });
                dataTemplatePancakeView.Content = pancakeChildLayout;

                //  dataTemplatePancakeView.ChildAdded(pancakeChildLayout); 
                vc.View = dataTemplatePancakeView;
                return vc;
            });
            statusRepeaterView.ItemTemplate = dataTemplate;
            statusRepeaterView.ItemsSource = productResponses.Take(5).ToList();
            statusRepeaterView.BindingContext = productResponses.ToList();
            statusScrollView.Content = statusRepeaterView;
            MainLayout.Children.Add(stackLayout);
            MainLayout.Children.Add(statusScrollView);
        }

    }
}
