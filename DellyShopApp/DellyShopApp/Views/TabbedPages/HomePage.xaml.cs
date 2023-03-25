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
                    ProductList = new string[] { "ip8_1", "ip8_2" } ,
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
            CategoryList.ItemsSource = DataService.Instance.CatoCategoriesList;//await DataService.GetCategories();
            CarouselView.ItemsSource = await DataService.GetAllCategories(orgId);//DataService.Instance.Carousel;
            BestSellerList.ItemsSource = await DataService.GetMostSellerProductsByOrganizations(orgId);//DataService.Instance.ProcutListModel;
            PreviousViewedList.ItemsSource = DataService.Instance.ProcutListModel;
            MostNews.FlowItemsSource = await DataService.GetAllProductsByOrganizations(orgId);//DataService.Instance.ProcutListModel;



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

        void BorderlessSearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
           
        }
       


    }
}