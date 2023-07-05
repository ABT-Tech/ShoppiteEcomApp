using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using DellyShopApp.Models;using System.Collections.Generic;using DellyShopApp.Views.TabbedPages;using System.Threading.Tasks;using System.Linq;using static DellyShopApp.Views.TabbedPages.BasketPage;using Xamarin.Essentials;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class AddNewAddress    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        public AddNewAddress(List<ChangeAddress> changeAddress)        {            InitializeComponent();

            AddTitle.ItemsSource = DataService.Instance.changeAddress;            statename.ItemsSource = DataService.Instance.changeAddress;            cityname.ItemsSource = DataService.Instance.changeAddress;            zipcode.ItemsSource = DataService.Instance.changeAddress;        }


        //private void ClosePageClick(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new BasketPage());
        //}


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)        {            var changeAddress = new ChangeAddress            {                AddressDetail = AddTitle.Text,                SelectState = statename.Text,                SelectCity = cityname.Text,                zipcode = zipcode.Text,                OrgId = orgId,                UserId = userId            };            var Page = new Page2(changeAddress);
            await DisplayAlert("Sucess", "Address Added Successfully", "ok");
            await Navigation.PopAsync();        }    }}