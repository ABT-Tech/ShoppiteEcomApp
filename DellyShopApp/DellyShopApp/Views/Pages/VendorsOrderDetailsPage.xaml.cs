using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using System;using System.Collections.Generic;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;
namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class VendorsOrderDetailsPage    {        List<ProductListModel> productListModel = new List<ProductListModel>();        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private readonly BasketPageVm _basketVm = new BasketPageVm();
        private Page2 page;        private List<ProductListModel> Product { get; set; }        public VendorsOrderDetailsPage(List<ProductListModel> product)        {
            this.Product = product;            InitializeComponent();            InittBasketPage();            this.BindingContext = product;
        }        public VendorsOrderDetailsPage()        {
            InitializeComponent();            InittBasketPage();
            PickerDemo.ItemsSource = new List<string>
        {
            "Pending",
            "Complete",
            "Cancelled",
            "Cancellation",
            "Packaged"
        };        }        public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;            }        }        private async void InittBasketPage()        {
            productListModel = DataService.Instance.ProcutListModel.ToList();            BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
            BasketItems.ItemsSource = DataService.Instance.orderdetails.ProductLists;
            lblDate.Text = DataService.Instance.orderdetails.Date;
            lblAddress.Text = DataService.Instance.orderdetails.Address;
            //productListModel = await DataService.GetAllCartDetails(orgId, userId);
            //BasketItems.ItemsSource = productListModel;
        }        protected override async void OnAppearing()        {            base.OnAppearing();
            this.BindingContext = Product;            BasketItems.ItemsSource = productListModel;//await DataService.GetAllCartDetails(orgId, userId);
            DataService.Instance.BaseTotalPrice = 0;            DataService.Instance.TotalPrice = 0;            foreach (var product in productListModel)
            {
                DataService.Instance.BaseTotalPrice += product.Quantity * product.Price;                DataService.Instance.TotalPrice += product.Quantity * product.Price;
                //return;
                //{
                //   DataService.Instance.BaseTotalPrice = product.Quantity * product.Price;
                //}
            }
            TotalPrice.Text = $"{ DataService.Instance.BaseTotalPrice}₹";
        }        private async void AddAddressClick(object sender, EventArgs e)        {
            await Navigation.PushModalAsync(new AddNewAddressPage(DataService.Instance.changeAddress.ToList()));        }        private async void ContinueClick(object sender, EventArgs e)        {            OrderCheckOut orderCheckOut = new OrderCheckOut();            orderCheckOut.ProductLists = productListModel;            orderCheckOut.orgid = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);            orderCheckOut.BaseTotalPrice = ((decimal)DataService.Instance.BaseTotalPrice);            orderCheckOut.TotalPrice = ((decimal)DataService.Instance.TotalPrice + 12);
            //orderCheckOut.Contactnumber = ((decimal)DataService.Instance.Contactnumber);
            await DataService.Checkout(orderCheckOut);            await Navigation.PushAsync(new SuccessPage(productListModel));        }       
        private async void Submitclick(object sender, EventArgs e)
        {
            var orders = new Orders            {
                Remark = VendorRemark.Text,
                orgId = orgId,
                UserId = userId,
                orderstatus = (string)PickerDemo.SelectedItem
        };

            await DataService.Submit(orders);
            await DisplayAlert("Done", "submited", "ok");
            await Navigation.PushAsync(new VendorsOrderPage());
        }

        private void Log_outclick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
            Xamarin.Essentials.SecureStorage.RemoveAll();
        }
    }}