using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.Pages;using System;using System.Linq;
using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class CustomerListPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private bool _open = false;        public CustomerListPage()        {            InitializeComponent();            InittCustomerListPage();        }        private async void InittCustomerListPage()        {
            var Cumlist = DataService.Instance.customerInfo;
            Cumlist = Cumlist.Where(X => X.Active == true).ToList();
            BasketItems.ItemsSource = Cumlist;
        }
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)        {            SearchBar searchBar = (SearchBar)sender;            if (searchBar.Text != "")            {                searchResults.IsVisible = true;                searchResults.ItemsSource = DataService.Instance.customerInfo;

            }            else            {                searchResults.IsVisible = false;            }
        }        private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)        {            var type = sender.GetType();            var evnt = (CustomerInfo)searchResults.SelectedItem;            //await Navigation.PushAsync(new ProductDetail(evnt));        }
        void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Sucess", "Customer is Blacklisted", "Ok");
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            DisplayAlert("Sucess", "Customer is whitelisted", "Ok");
        }
        private void Check_CheckedChanged(object sender, CheckedChangedEventArgs e)        {             var list = DataService.Instance.customerInfo;            CheckBox checkBox = (CheckBox)sender;            if (!checkBox.IsChecked)                list = list.Where(X => X.Active == false).ToList();            else                list = list.Where(X => X.Active == true).ToList();            BasketItems.ItemsSource = list;        }
    }}