using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using Plugin.Connectivity;
using System;using System.Collections.Generic;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class UserOrderDetails    {        List<OrderListModel> orderListModel = new List<OrderListModel>();        List<ProductListModel> productListModel = new List<ProductListModel>();        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        //public int orderId = Convert.ToInt32(SecureStorage.GetAsync("orderId").Result);
        public int OrderMasterId = Convert.ToInt32(SecureStorage.GetAsync("OrderMasterId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        public string remark = SecureStorage.GetAsync("Remark").Result;



        private List<OrderListModel> Product { get; set; }        private List<ProductListModel> product { get; set; }        public UserOrderDetails(int orderId)        {            OrderMasterId = orderId;            InitializeComponent();            if (ChechConnectivity())
            {
                InittBasketPage();
            }            this.BindingContext = product;        }
        private bool ChechConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");
                return false;
            }
        }        public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;             }        }        private async void InittBasketPage()        {            Busy();            var orderDetails = await DataService.GetOrderDetailsByOrderMasterId(orgId, OrderMasterId);            orderListModel = orderDetails.ProductLists.ToList();            BasketItems.ItemsSource = orderDetails.ProductLists;//DataService.Instance.orderdetails.ProductLists;
            lblDate.Text = orderDetails.Date;//DataService.Instance.orderdetails.Date;
            lblstatus.Text = orderDetails.ProductLists.FirstOrDefault().orderStatus;            lblno.Text = OrderMasterId.ToString();            lblAddress.Text = orderDetails.Address;            DataService.Instance.TotalPrice = 0;             foreach (var product in orderListModel)            {                DataService.Instance.TotalPrice += product.Quantity * product.Price;            }            TotalPrice.Text = $"{ DataService.Instance.TotalPrice}₹";            if(orderDetails.ProductLists.FirstOrDefault().orderStatus == "Pending")
            {
                cancelbutton.IsVisible = true;
                canceltxtbox.IsVisible = true;
            }            NotBusy();
                  }        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            MainStack.Opacity = 0.7;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            MainStack.Opacity = 100;
        }        private async void AddAddressClick(object sender, EventArgs e)        {            await Navigation.PushModalAsync(new AddNewAddressPage(DataService.Instance.changeAddress.ToList()));        }        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }        async void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)        {
            {
                var canselorder = new Cancelorder
                {
                    orgId = orgId,
                    Reason = reason.Text,
                    orderstatus = lblstatus.Text,
                    OrderId = OrderMasterId,


                };
                await DataService.Cancel(canselorder);
                await DisplayAlert("Done", "Your Order Is Cancel", "Ok");
                await Navigation.PushAsync(new MyOrderPage());

            }        }    }}