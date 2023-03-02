using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        ProductListModel product = new ProductListModel();

        public Category C { get; private set; }
        public object ProcutListModel { get; private set; }

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        private bool x;

        public HomePage()
        {
            if (!DataService.Instance.ProcutListModel.Any(x => x.Id == 4))
            {
                DataService.Instance.ProcutListModel.Insert(0, new ProductListModel
                {
                    Title = AppResources.ProcutTitle3,
                    Brand = AppResources.ProductBrand3,

                    Id = 4,
                    Image = "iphone",
                    Price = 499,
                    OldPrice = 699,
                    VisibleItemDelete = false,
                    ProductList = new string[] { "ip8_1", "ip8_2" } ,
                    orgId = 1
                });
            }
            InitializeComponent();
            ShopLogo.Source = DataService.Instance.ObjOrgData.Image;
           
            InittHomePage();
          
        }

        private async void InittHomePage()
        {
            CategoryList.ItemsSource = await DataService.GetCategories(orgId);//DataService.Instance.CatoCategoriesList.Where(x => x.orgID == orgId);
            CarouselView.ItemsSource =DataService.Instance.Carousel.Where(x => x.orgID == orgId);
            BestSellerList.ItemsSource =DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            PreviousViewedList.ItemsSource = await DataService.GetAllProductsByOrganizations(orgId);//DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            MostNews.FlowItemsSource = DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId).ToList();

        }

        private async void ProductDetailClick(object sender, EventArgs e)
        {

            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
            
           // await Shell.Current.GoToAsync(nameof(ProductDetail));
           
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

        void BorderlessSearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            
            
                //SearchBar searchBar = (SearchBar)sender;
                //ProductListModel.Insource = DataService.(searchBar.Text);

                //SearchBar searchBar = new SearchBar { Placeholder = "Search items..." };
                //var Searchbar = ProcutListModel.Add(c => c.ProductListModel.Cotains(SearchBar.Text));
            
        }

    }
}