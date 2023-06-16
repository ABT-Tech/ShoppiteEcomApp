using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using System;using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class CustomerListPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private bool _open = false;
        public CustomerInfo _customerInfo;

        public CustomerListPage()        {            InitializeComponent();            InittCustomerListPage();        }        private async void InittCustomerListPage()        {            var Cumlist = await DataService.GetCustomerDetails(orgId);
            Cumlist = Cumlist.Where(X => X.Active == true).ToList();
            BasketItems.ItemsSource = Cumlist;
        }              private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)        {            SearchBar searchBar = (SearchBar)sender;            if (searchBar.Text != "")            {                searchResults.IsVisible = true;                searchResults.ItemsSource = DataService.Instance.customerInfo; //await DataService.SearchProducts(orgId, searchBar.Text);
            }            else            {                searchResults.IsVisible = false;            }        }        private async void searchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)        {            var type = sender.GetType();            var evnt = (CustomerInfo)searchResults.SelectedItem;
            //await Navigation.PushAsync(new CustomerListPage(evnt));
        }

      

        private async void Blacklisted(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            if (!(image.Parent.Parent.Parent.Parent.Parent is CustomControl.RepeaterView pancake)) return;
            if (!(pancake.BindingContext is CustomerInfo item)) return;
      

           // await DataService.UpdateUserStatus(orgId);
            DisplayAlert("Sucess", "Customer is Blacklisted", "Ok");
        }

        private void Whitelisted(object sender, EventArgs e)
        {
            DisplayAlert("Sucess", "Customer is Whitelisted", "Ok");
        }

        private async void Check_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var list = await DataService.GetCustomerDetails(orgId);
            CheckBox checkBox = (CheckBox)sender;
            if (!checkBox.IsChecked)
                list = list.Where(X => X.Active == false).ToList();
            else
                list = list.Where(X => X.Active == true).ToList();
            BasketItems.ItemsSource = list;


        }
    }}