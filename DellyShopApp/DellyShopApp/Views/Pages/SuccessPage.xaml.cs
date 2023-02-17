using System;
using System.Collections.Generic;
using DellyShopApp.Models;
using DellyShopApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPage
    {
        private List<ProductListModel> _procutListModel = new List<ProductListModel>();

        public SuccessPage()
        {
            InitializeComponent();
            BasketItems.ItemsSource = DataService.Instance.BasketModel;
        }

        private async void ContinueClick(object sender, EventArgs e)
        {
            //.Current.MainPage = new HomeTabbedPage();
            Application.Current.MainPage = new NavigationPage(new HomeTabbedPage());
        }
    }
}