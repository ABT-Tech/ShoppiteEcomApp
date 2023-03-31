using Acr.UserDialogs;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
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
        public int orderId = Convert.ToInt32(SecureStorage.GetAsync("orderId").Result);


        public Category C { get; private set; }
        public object ProcutListModel { get; private set; }


        private bool x;
        private ProductListModel product;

        public HomePage()
        {
            if (!DataService.Instance.ProcutListModel.Any(x => x.Id == 4))
            {
                DataService.Instance.ProcutListModel.Insert(0, new ProductListModel
                {
                    Title = AppResources.ProcutTitle3,
                    Brand = AppResources.ProductBrand3,
                    Quantity = 1,
                    UserId = 1,
                    Id = 4,
                    Image = "iphone",
                    Price = 499,
                    OldPrice = 699,
                    VisibleItemDelete = false,
                    ProductList = new string[] { "ip8_1", "ip8_2" },
                    orgId = 1,
                    orderId = 1
                });
            }
            InitializeComponent();
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;

            InittHomePage();
        }
        private async void InittHomePage()
        {
            CategoryList.ItemsSource = await DataService.GetCategories(orgId); //DataService.Instance.CatoCategoriesList.Where(x => x.orgID == orgId); //
            CarouselView.ItemsSource = await DataService.GetAllCategories(orgId); //DataService.Instance.Carousel.Where(x => x.orgID == orgId); //
            BestSellerList.ItemsSource = await DataService.GetMostSellerProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            PreviousViewedList.ItemsSource = await DataService.GetLastVisitedProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); //
            MostNews.FlowItemsSource = await DataService.GetAllProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId).ToList(); //
        }
        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
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
            Category Ca = DataService.Instance.CatoCategoriesList.Where(x => x.CategoryId == c.CategoryId).FirstOrDefault();
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
