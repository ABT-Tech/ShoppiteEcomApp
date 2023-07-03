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
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        List<Category> categories = new List<Category>();
        public List<Products> ProductDetails { get; set; }
        private ProductListModel product;

        public HomePage()
        {
            InitializeComponent();            
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;            
        }
        private async void InittHomePage()
        {
            int? OrgUserID = UserId == 0 ? null : (int?)UserId;
            categories = await DataService.GetAllCategories(orgId);
            CategoryList.ItemsSource = categories; //DataService.Instance.CatoCategoriesList.Where(x => x.orgID == orgId); //
            CarouselView.ItemsSource = categories.Where(x => x.Banner != null && x.Banner!="").ToList(); //DataService.Instance.Carousel.Where(x => x.orgID == orgId); //
            BestSellerList.ItemsSource = await DataService.GetMostSellerProductsByOrganizations(orgId,OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            PreviousViewedList.ItemsSource = await DataService.GetLastVisitedProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); //
            MostNews.FlowItemsSource = await DataService.GetAllProductsByOrganizations(orgId, OrgUserID); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId).ToList(); //
            var products = DataService.Instance.products;
            foreach (var pro in products)
            {
                var label = new Label
                {
                    Text = pro.Stutus,
                    Margin = 10,
                    Padding = 5,
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "{DynamicResource VerdanaProBold}",
                    FontSize = 18,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };
                var label2 = new Label
                {
                    Text = pro.all,
                    Margin = 10,
                    Padding = 5,
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "{DynamicResource VerdanaProBold}",
                    FontSize = 16,
                    TextColor = Color.Gray,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.End
                };

                var stacklayout = new StackLayout();
                stacklayout.Children.Add(label);
                stacklayout.Children.Add(label2);
                var pancake = new PancakeView();

                var stacklayout1 = new StackLayout();
                foreach (var prolist in pro.productListModel)
                {
                    var image = new Image
                    {
                        Source = prolist.Image,
                        Aspect = Aspect.AspectFill,
                        HeightRequest = 100,
                        WidthRequest = 100,
                        Margin = 5
                    };
                    var label3 = new Label
                    {
                        Text = prolist.Title,
                        FontFamily = "{ DynamicResource VerdanaProRegular }",
                        MaxLines = 1,
                        LineBreakMode = LineBreakMode.TailTruncation,
                        TextColor = Color.Black,
                        VerticalOptions = LayoutOptions.End
                    };
                    var label4 = new Label
                    {
                        Text = prolist.Brand,
                        FontFamily = "{DynamicResource VerdanaProRegular}",
                        FontSize = 10,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Color.Black
                    };
                    var label5 = new Label
                    {
                        Text = prolist.OldPrice.ToString() + " ₹",
                        FontFamily = "{DynamicResource VerdanaProBold}",
                        FontSize = 18,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        TextColor = Color.Gray,
                        WidthRequest = 80,
                        TextDecorations = TextDecorations.Strikethrough,
                        VerticalOptions = LayoutOptions.End
                    };
                    var label6 = new Label
                    {
                        Text = prolist.Price.ToString() + " ₹",
                        FontFamily = "{DynamicResource VerdanaProBold}",
                        FontSize = 13,
                        HorizontalOptions = LayoutOptions.End,
                        TextColor = Color.Black,
                        WidthRequest = 80,
                        VerticalOptions = LayoutOptions.End
                    };
                    stacklayout1.Children.Add(image);
                    stacklayout1.Children.Add(label3);
                    stacklayout1.Children.Add(label4);
                    stacklayout1.Children.Add(label5);
                    stacklayout1.Children.Add(label6);
                    
                }

                // ////HomeMainStack
                // var scrollview = new ScrollView();
                //var repeterview = new CustomControl.RepeaterView();
                //repeterview.ItemTemplate = new DataTemplate();
                // ////repeterview.ItemTemplate.
                // ////repeterview.Children.Add(Image);
                //repeterview.ItemsSource = pro.productListModel;
                HomeMainStack.Children.Add(stacklayout);
                stacklayout.Orientation = StackOrientation.Horizontal;
                //HomeMainStack.Children.Add(repeterview);
                HomeMainStack.Children.Add(stacklayout1);
                
            }

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
        void DropBasketITem(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            if (!DataService.Instance.BasketModel.Contains(product))
                DataService.Instance.BasketModel.Add(product);
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
    }
}
