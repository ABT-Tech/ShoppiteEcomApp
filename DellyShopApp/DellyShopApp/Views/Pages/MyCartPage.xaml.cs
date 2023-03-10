using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.TabbedPages;using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;using DellyShopApp.ViewModel;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class MyCartPage
    {
        ProductListModel Product = new ProductListModel();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
             int MyCartCountLable;
             

            public MyCartPage()        {            InitializeComponent();            BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
            InittMyCartPage();
                  }
        private async void InittMyCartPage()        {
            

        }

        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;
            int Id = DataService.Instance.order.orgId;
            int UserId = DataService.Instance.order.UserId;
            await Navigation.PushAsync(new ProductDetail(item));        }        private async void Button_Clicked(object sender, EventArgs e)        {            await Navigation.PushAsync(new BasketPage(DataService.Instance.ProcutListModel.ToList()));        }        private void PlusClick(object sender, EventArgs e)        {            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            if (CurrentQuantity >= 10) return;            MyCartCountLable.Text = (++CurrentQuantity).ToString();        }        private void MinusClick(object sender, EventArgs e)        {            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            if (CurrentQuantity == 1) return;

            MyCartCountLable.Text = (--CurrentQuantity).ToString();        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {
                // NavbarStack.IsVisible = true;
                return;            }
            // NavbarStack.IsVisible = false;
        }    }}