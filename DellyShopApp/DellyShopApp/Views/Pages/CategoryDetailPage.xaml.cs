using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage
    {
        public CategoryDetailPage(Category category)
        {
            InitializeComponent();
            this.BindingContext = category;
            CarouselView.ItemsSource = DataService.Instance.CatoCategoriesDetail;
            BestSellerList.ItemsSource = DataService.Instance.ProcutListModel.Where(x => x.Id != 4);
            PreviousViewedList.ItemsSource = DataService.Instance.ProcutListModel.Where(x=>x.Id!=4);
            MostNews.FlowItemsSource = DataService.Instance.ProcutListModel.Where(x=>x.Id!=4).ToList();
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
    }
}