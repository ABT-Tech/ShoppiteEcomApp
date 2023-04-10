using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using DellyShopApp.Models;using System.Collections.Generic;using DellyShopApp.Views.TabbedPages;using System.Threading.Tasks;
using System.Linq;
using static DellyShopApp.Views.TabbedPages.BasketPage;

namespace DellyShopApp.Views.ModalPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class AddNewAddressPage    {
        public AddNewAddressPage(List<ChangeAddress> changeAddress)        {            InitializeComponent();                       AddTitle.ItemsSource = DataService.Instance.changeAddress;            SelCnty.ItemsSource = DataService.Instance.changeAddress;            SelCity.ItemsSource = DataService.Instance.changeAddress;            SelStreet.ItemsSource = DataService.Instance.changeAddress;            AddDetail.ItemsSource = DataService.Instance.changeAddress;

        }

        
        //private void ClosePageClick(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new BasketPage());
        //}


        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var changeAddress = new ChangeAddress            {                //AddressTitle = AddTitle.Text,                //SelectCountry = SelCnty.Text,                //SelectCity = SelCity.Text,                //SelectStreet = SelStreet.Text,                //AddressDetail = AddDetail.Text,                                          };            var Page = new Page2(changeAddress);

            DisplayAlert("Sucess", "Address Added Successfully", "ok");
            Navigation.PushAsync(new BasketPage(Page));
        }

        }
}