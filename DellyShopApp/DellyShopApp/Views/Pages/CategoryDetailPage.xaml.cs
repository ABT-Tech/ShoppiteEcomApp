using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.Session.MediaSession;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage
    {
        public object C { get; private set; }
        public int CategoryId = Convert.ToInt32(SecureStorage.GetAsync("CategoryId").Result);
        ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);

        public CategoryDetailPage(Category category)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(category.Banner) )
            {
                CarouselView.IsVisible = false;
            }
            else
            {
                CarouselView.IsVisible = true;
            }
            InitCategoryPage(category);
        }
       
        public CategoryDetailPage()
        {
        }

        public async void InitCategoryPage(Category category) 
        {
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result;
            await Xamarin.Essentials.SecureStorage.SetAsync("CategoryId", category.CategoryId);
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result; //DataService.Instance.ObjOrgData.Image;
            var AllCarosol = await DataService.GetAllCategories(orgId);
            //var AllBestSeller = await DataService.GetMostSellerProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId);
            //var AllPrevious = await DataService.GetLastVisitedProductsByOrganizations(orgId); //DataService.Instance.ProcutListModel.Where(x => x.orgId == orgId); //
            var AllMostSeller = await DataService.GetAllProductsByOrganizations(orgId);
            var CarosalList = await DataService.GetAllCategories(orgId);
            CarouselView.ItemsSource = CarosalList.Where(x => x.Banner != null && x.Banner != "").ToList();
            CarouselView.ItemsSource = AllCarosol.Where(x => x.CategoryId == category.CategoryId).ToList();
            //BestSellerList.ItemsSource = AllBestSeller;
            //PreviousViewedList.ItemsSource = AllPrevious;
            MostNews.FlowItemsSource = AllMostSeller;
            await Xamarin.Essentials.SecureStorage.SetAsync("CategoryId", category.CategoryId);
        }

        private async void ClickCategory(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stack)) return;
            if (!(stack.BindingContext is ProductListModel ca)) return;
            await Navigation.PushAsync(new ProductDetail(ca));
        }
        private async void BackPage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void ProductDetailClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }

        private async void BannerTab(object sender, EventArgs e)
        {
            if (!(sender is ContentView content)) return;
            if (!(content.BindingContext is Category c)) return;
            Category Ca = DataService.Instance.CatoCategoriesList.Where(x => x.Banner != null && x.Banner != "").FirstOrDefault();

            await Navigation.PushAsync(new HomeTabbedPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new MainPage());           
        }
    }
}