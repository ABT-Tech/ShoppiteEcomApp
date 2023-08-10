using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using Plugin.Connectivity;using System;using System.Collections.Generic;using System.Diagnostics;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class BasketPage    {        List<ProductListModel> productListModel = new List<ProductListModel>();        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);


        private readonly BasketPageVm _basketVm = new BasketPageVm();        private int _quantity;        private Page2 page;        private List<ProductListModel> Product { get; set; }        public BasketPage(List<ProductListModel> product)        {            this.Product = product;            InitializeComponent();            if (ChechConnectivity())            {                InittBasketPage();            }

            this.BindingContext = product;

        }        private bool ChechConnectivity()        {            if (CrossConnectivity.Current.IsConnected)            {                return true;            }            else            {                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");                return false;            }        }        public BasketPage(Page2 page)        {            this.page = page;        }        public BasketPage()        {        }


        public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;            }        }        private async void InittBasketPage()        {            Busy();            productListModel = this.Product;            var payment = await DataService.GetOnePlayFlag(orgId);            if (payment.OnePay != false)            {                onepay.IsVisible = true;            }            else            {                onepay.IsVisible = false;            }            BasketItems.ItemsSource = this.Product;//await DataService.GetAllCartDetails(orgId, userId);//DataService.Instance.ProcutListModel;
            AddressPicker.ItemsSource = await DataService.GetAddressByUserId(orgId, userId); //DataService.Instance.changeAddress;
            BasketItems.ItemsSource = productListModel;
            NotBusy();


        }        public void Busy()        {            uploadIndicator.IsVisible = true;            uploadIndicator.IsRunning = true;

        }        public void NotBusy()        {            uploadIndicator.IsVisible = false;            uploadIndicator.IsRunning = false;

        }
        protected override async void OnAppearing()        {            base.OnAppearing();            this.BindingContext = Product;            BasketItems.ItemsSource = productListModel;//await DataService.GetAllCartDetails(orgId, userId);
            DataService.Instance.TotalPrice = 0;


            foreach (var product in productListModel)            {                DataService.Instance.TotalPrice += product.Quantity * product.Price;
                //DataService.Instance.TotalPrice += product.Quantity * product.Price;

                //return;
                //{
                //   DataService.Instance.BaseTotalPrice = product.Quantity * product.Price;
                //}
            }

            //SubTotal.Text = $"{ DataService.Instance.BaseTotalPrice }₹";
            TotalPrice.Text = $"{DataService.Instance.TotalPrice}₹";        }
        /// <summary>        /// Go to Address Page        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>                                                                                                                                                                   private async void AddAddressClick(object sender, EventArgs e)        {            await Navigation.PushModalAsync(new AddNewAddressPage(DataService.Instance.changeAddress.ToList()));        }        private async void ContinueClick(object sender, EventArgs e)        {            OrderCheckOut orderCheckOut = new OrderCheckOut();            orderCheckOut.ProductLists = productListModel;            orderCheckOut.orgid = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);            orderCheckOut.userid = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);            orderCheckOut.Address = (ChangeAddress)AddressPicker.SelectedItem;            orderCheckOut.TotalPrice = ((decimal)DataService.Instance.TotalPrice);            orderCheckOut.OrderGuid = productListModel.FirstOrDefault().OrderGuId;            if (orderCheckOut.Address == null)            {                await DisplayAlert("Opps!", "Please Select Address", "ok");                return;            }
            //await Navigation.PushAsync(new PaymentPage(productListModel));
            await DataService.Checkout(orderCheckOut);            await Navigation.PushAsync(new SuccessPage());        }        private async void BrowserUrl(System.Object sender, System.EventArgs e)        {            OrderCheckOut orderCheckOut = new OrderCheckOut();            orderCheckOut.ProductLists = productListModel;            orderCheckOut.orgid = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);            orderCheckOut.userid = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);            orderCheckOut.Address = (ChangeAddress)AddressPicker.SelectedItem;            orderCheckOut.TotalPrice = ((decimal)DataService.Instance.TotalPrice);            orderCheckOut.OrderGuid = productListModel.FirstOrDefault().OrderGuId;            orderCheckOut.OnePay = true;            if (orderCheckOut.Address == null)            {                await DisplayAlert("Opps!", "Please Select Address", "Ok");                return;            }
            //await Navigation.PushAsync(new PaymentPage(productListModel));
            var pay = await DataService.MakePaymentRequest(orderCheckOut);
            await SecureStorage.SetAsync("PaymentUrl", pay.AggregatorCallbackURL);            DataService dataService = new DataService();

            Content = new StackLayout            {                Children =                     {                        new MyWebview()                       {                            url="https://pa-preprod.1pay.in/payment/payprocessorV2",                            WidthRequest = 300,                            HeightRequest = 900,                            data = string.Format("merchantId={0}&reqData={1}", pay.merchantId, pay.encryptedParams)
                       },                  },                VerticalOptions = LayoutOptions.FillAndExpand,                HorizontalOptions = LayoutOptions.FillAndExpand            };        }
        /// <summary>        /// Delete Visible Settings        /// </summary        /// <param name="sender"></param>        /// <param name="e"></param>                                                                                                                                                                                                                                                                                                                                      private void DeleteItemSwipe(object sender, SwipedEventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (pancake.BindingContext is ProductListModel item)            {                item.VisibleItemDelete = true;                VisibleDelete(item.Id);            }        }
        /// <summary>        /// Delete Visible Settings        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>                                                                                                                                                                                                                                                                                                                                        private void UndeleteI(object sender, SwipedEventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (pancake.BindingContext is ProductListModel item)            {                item.VisibleItemDelete = false;                VisibleDelete(item.Id);            }        }        private void VisibleDelete(int id)        {            var items = _basketVm.ProcutListModel.Where(x => x.Id != id);            foreach (var item in items)            {                item.VisibleItemDelete = false;            }        }        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }
    }}