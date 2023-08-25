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
        //public int Org_CategoryId = Convert.ToInt32(SecureStorage.GetAsync(" Org_CategoryId").Result);

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
      
        public CategoryDetailPage(Category category)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(category.Banner) )
            {
                CategoryBannerPancake.IsVisible = false;
            }
            else
            {
                CategoryBannerPancake.IsVisible = true;
            }
            InitCategoryPage(category);
        }
       
        public async void InitCategoryPage(Category category) 
        {
            Busy();
            List<Category> categories = new List<Category>();
            categories.Add(category);
            var AllMostSeller = await DataService.GetAllProductsByCategory(orgId,Convert.ToInt32(category.CategoryId));
            if(AllMostSeller.Count == 0)
            {
                gif.IsVisible = true;
                lbl.IsVisible = false;
            }
            StackLabelCategoryName.Text = category.CategoryName;
            ShopLogo.Source = SecureStorage.GetAsync("ImgId").Result;
            CarouselView.ItemsSource = categories;
            MostNews.FlowItemsSource = AllMostSeller;
            NotBusy();
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());           
        }
    }
}                                                                            