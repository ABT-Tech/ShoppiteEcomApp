using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.Pages;
using System;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class CustomerListPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private bool _open = false;        public CustomerListPage()        {            InitializeComponent();            InittCustomerListPage();        }        private async void InittCustomerListPage()        {            BasketItems.ItemsSource = DataService.Instance.customerInfo; //await DataService.GetMyOrderDetails(orgId, userId);//
        }        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)        {            SearchBar searchBar = (SearchBar)sender;            if (searchBar.Text != "")            {                searchResults.IsVisible = true;                searchResults.ItemsSource = DataService.Instance.customerInfo; //await DataService.SearchProducts(orgId, searchBar.Text);
            }            else            {                searchResults.IsVisible = false;            }        }        private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)        {            var type = sender.GetType();            var evnt = (CustomerInfo)searchResults.SelectedItem;
            //await Navigation.PushAsync(new CustomerListPage(evnt));
        }

        private void Greenimg(object sender, EventArgs e)
        {
            Image imageSource = (Image)sender;
            if (!(imageSource.Parent.Parent.Parent is StackLayout stack)) return;
        }

        private void Redimg(object sender, EventArgs e)
        {

        }
    }}