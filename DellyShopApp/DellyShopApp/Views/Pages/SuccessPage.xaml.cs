using DellyShopApp.Models;
using DellyShopApp.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPage
    {
        private List<Order> _procutListModel = new List<Order>();

        public List<ProductListModel> Product { get; }

        public SuccessPage(List<ProductListModel> product)
        {
            InitializeComponent();
            InittSuccessPage();
            this.Product = product;
            this.BindingContext = product;
            BasketItems.ItemsSource = DataService.Instance.BasketModel;
            foreach (var item in DataService.Instance.ProcutListModel)
            {
                DataService.Instance.BaseTotalPrice += item.Price;

            }
        }

        public SuccessPage()
        {
        }

        private async void InittSuccessPage()
        {
            //BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
        }

        private async void ContinueClick(object sender, EventArgs e)
        {
            //.Current.MainPage = new HomeTabbedPage();
            Application.Current.MainPage = new NavigationPage(new HomeTabbedPage());
        }
    }
}