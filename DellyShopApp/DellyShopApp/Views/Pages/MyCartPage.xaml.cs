using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.TabbedPages;using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;using DellyShopApp.ViewModel;using Xamarin.Forms;using Xamarin.Forms.Xaml;using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class MyCartPage
    {
        ProductListModel ProductListModel = new ProductListModel();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
      


        int MyCartCountLable;

        public object Product { get; private set; }

        public MyCartPage()        {
            InitializeComponent();
            InittMyCartPage();
        }       
        private async void InittMyCartPage()        {
            BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
        }                    
        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;
            int Id = DataService.Instance.order.orgId;
            int UserId = DataService.Instance.order.UserId;
            await Navigation.PushAsync(new ProductDetail(item));        }        private async void Button_Clicked(object sender, EventArgs e)        {            await Navigation.PushAsync(new BasketPage(DataService.Instance.ProcutListModel.ToList()));        }        private async void PlusClick(object sender, EventArgs e)        {
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            if (CurrentQuantity >= 10) return;            MyCartCountLable.Text = (++CurrentQuantity).ToString();            Label CartSelectedProduct = (Label)repaterStack.Children[2];
            var products = DataService.Instance.ProcutListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = CurrentQuantity;        
        }        private async void MinusClick(object sender, EventArgs e)        { 
            Image image = (Image)sender;            StackLayout repaterStack = (StackLayout)image.Parent;            Label MyCartCountLable = (Label)repaterStack.Children[1];            int CurrentQuantity = Convert.ToInt32(MyCartCountLable.Text);            Label CartSelectedProduct = (Label)repaterStack.Children[2];            if (CurrentQuantity == 1) return;
            MyCartCountLable.Text = (--CurrentQuantity).ToString();
            var products = DataService.Instance.ProcutListModel.Where(x => x.Id == Convert.ToInt32(CartSelectedProduct.Text)).FirstOrDefault();
            products.Quantity = CurrentQuantity;
         
        }        private void MainScroll_Scrolled(object sender, ScrolledEventArgs e)        {            var height = Math.Round(Application.Current.MainPage.Height);            var ycordinate = Math.Round(e.ScrollY);            if (ycordinate > (height / 3))            {
                // NavbarStack.IsVisible = true;
                return;            }
            // NavbarStack.IsVisible = false;
        }    }}