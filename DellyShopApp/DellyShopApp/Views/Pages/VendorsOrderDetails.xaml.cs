using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using PayPal.Forms;using PayPal.Forms.Abstractions;using Plugin.Connectivity;
using System;using System.Collections.Generic;using System.Diagnostics;using System.Linq;using System.Threading.Tasks;
using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class VendorsOrderDetails    {        List<OrderListModel> orderListModel = new List<OrderListModel>();
     

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int orderId = Convert.ToInt32(SecureStorage.GetAsync("OrderId").Result);

        public int OrderMasterId = Convert.ToInt32(SecureStorage.GetAsync("OrderMasterId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public string remark = SecureStorage.GetAsync("Remark").Result;        



        private Page2 page;        private int OrderID { get; set; }        public VendorsOrderDetails(int orderID)        {            OrderMasterId = orderID;            InitializeComponent();
            if (ChechConnectivity())
            {
                InittBasketPage();
            }            
            PickerDemo.ItemsSource = new List<string>
            {
            "Pending",            "Delivered",
            "Cancelled",
            "Out for Delivery",
            "Request Cancellation"

            };        }
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
        }        public VendorsOrderDetails()        {            InitializeComponent();            InittBasketPage();
        }                  
       
        public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;            }        }        private async void InittBasketPage()        {
            var orderDetails = await DataService.GetOrderDetailsByOrderMasterId(orgId, OrderMasterId);
            orderListModel = orderDetails.ProductLists.ToList();
            BasketItems.ItemsSource = orderDetails.ProductLists;//DataService.Instance.orderdetails.ProductLists;
            lblDate.Text = orderDetails.Date;//DataService.Instance.orderdetails.Date;
            lblAddress.Text = orderDetails.Address;//DataService.Instance.orderdetails.Address;
            DataService.Instance.TotalPrice = 0;
            foreach (var product in orderListModel)                       {
                               DataService.Instance.TotalPrice += product.Quantity * product.Price;              //DataService.Instance.TotalPrice += product.Quantity * product.Price;
            }                        TotalPrice.Text = $"{ DataService.Instance.TotalPrice}₹";
            PickerDemo.SelectedItem = orderDetails.ProductLists.FirstOrDefault().orderStatus;
           
        }                private async void AddAddressClick(object sender, EventArgs e)        {            await Navigation.PushModalAsync(new AddNewAddressPage(DataService.Instance.changeAddress.ToList()));        }               private async void submit_click(object sender, EventArgs e)        {
            var orders = new Orders
            {                orgId = orgId,                Remark = vendorremark.Text,                orderstatus = (string)PickerDemo.SelectedItem,                orderId = OrderMasterId,                UserId = userId

            };

            await DataService.Submit(orders);
            await DisplayAlert("Done", "Submited", "Ok");
            await Navigation.PushAsync(new Venderdata());
        }
        private async void Log_outclick(object sender, EventArgs e)        {            await Navigation.PushAsync(new MainPage());
            Xamarin.Essentials.SecureStorage.RemoveAll();        }

        

      async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            
        }
    }
}