using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using Foundation;
using Newtonsoft.Json;
using PayPal.Forms;using PayPal.Forms.Abstractions;using Plugin.Connectivity;
using System;using System.Collections.Generic;using System.Diagnostics;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class BasketPage    {        List<ProductListModel> productListModel = new List<ProductListModel>();        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private readonly BasketPageVm _basketVm = new BasketPageVm();        private int _quantity;        private Page2 page;        private List<ProductListModel> Product { get; set; }        public BasketPage(List<ProductListModel> product)        {
            this.Product = product;            InitializeComponent();                      if (ChechConnectivity())
            {
                InittBasketPage();
            }            this.BindingContext = product;
        }
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
        }        public BasketPage(Page2 page)        {            this.page = page;        }        public BasketPage()        {        }        public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;            }        }        private async void InittBasketPage()        {            productListModel = this.Product;            BasketItems.ItemsSource = this.Product;//await DataService.GetAllCartDetails(orgId, userId);//DataService.Instance.ProcutListModel;
            var Add = await DataService.GetAddressByUserId(orgId, userId);                        AddressPicker.ItemsSource = Add; //DataService.Instance.changeAddress;
            //if (Add != null)
            //{
            //    SelAdd.IsVisible = false;
            //}
            //productListModel = await DataService.GetAllCartDetails(orgId, userId);
            BasketItems.ItemsSource = productListModel;
           
        }        protected override async void OnAppearing()        {            base.OnAppearing();

            this.BindingContext = Product;            BasketItems.ItemsSource = productListModel;//await DataService.GetAllCartDetails(orgId, userId);           // DataService.Instance.BaseTotalPrice = 0;            DataService.Instance.TotalPrice = 0;            foreach (var product in productListModel)
            {
                DataService.Instance.TotalPrice += product.Quantity * product.Price;               
            }
            TotalPrice.Text = $"{ DataService.Instance.TotalPrice }₹";
        }
        /// <summary>        /// Go to Address Page        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        private async void AddAddressClick(object sender, EventArgs e)        {
                   }        private async void ContinueClick(object sender, EventArgs e)        {            OrderCheckOut orderCheckOut = new OrderCheckOut();            orderCheckOut.ProductLists = productListModel;            orderCheckOut.orgid = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);            orderCheckOut.userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);            orderCheckOut.Address = (ChangeAddress)AddressPicker.SelectedItem;            //orderCheckOut.BaseTotalPrice = ((decimal)DataService.Instance.TotalPrice);            orderCheckOut.TotalPrice = ((decimal)DataService.Instance.TotalPrice);            orderCheckOut.OrderGuid = productListModel.FirstOrDefault().OrderGuId;            if (orderCheckOut.Address == null)
            {
                await DisplayAlert("Opps!", "Please Select Address", "ok");
                return;
            }
            await  Navigation.PushAsync(new PaymentPage(productListModel));            //await DataService.Checkout(orderCheckOut);            //await Navigation.PushAsync(new SuccessPage(productListModel));                   }
        /// <summary>        /// Delete Visible Settings        /// </summary        /// <param name="sender"></param>        /// <param name="e"></param>        private void DeleteItemSwipe(object sender, SwipedEventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (pancake.BindingContext is ProductListModel item)            {                item.VisibleItemDelete = true;                VisibleDelete(item.Id);            }        }
        /// <summary>        /// Delete Visible Settings        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        private void UndeleteI(object sender, SwipedEventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (pancake.BindingContext is ProductListModel item)            {                item.VisibleItemDelete = false;                VisibleDelete(item.Id);            }        }        private void VisibleDelete(int id)        {            var items = _basketVm.ProcutListModel.Where(x => x.Id != id);            foreach (var item in items)            {                item.VisibleItemDelete = false;            }        }        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }        async void ContinueWithPaypal(System.Object sender, System.EventArgs e)        {
            //Single Item
            var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(12.50), "USD"), new Decimal(0));            if (result.Status == PayPalStatus.Cancelled)            {                Debug.WriteLine("Cancelled");            }            else if (result.Status == PayPalStatus.Error)            {                Debug.WriteLine(result.ErrorMessage);            }            else if (result.Status == PayPalStatus.Successful)            {                Debug.WriteLine(result.ServerResponse.Response.Id);            }

            #region List of Items            //var resultList = await CrossPayPalManager.Current.Buy(new PayPalItem[] {
                                              //    new PayPalItem ("sample item #1", 2, new Decimal (87.50), "USD",
                                              //        "sku-12345678"),
                                              //    new PayPalItem ("free sample item #2", 1, new Decimal (0.00),
                                              //        "USD", "sku-zero-price"),
                                              //    new PayPalItem ("sample item #3 with a longer name", 6, new Decimal (37.99),
                                              //        "USD", "sku-33333")
                                              //}, new Decimal(20.5), new Decimal(13.20));
                                              //if (result.Status == PayPalStatus.Cancelled)
                                              //{
                                              //    Debug.WriteLine("Cancelled");
                                              //}
                                              //else if (result.Status == PayPalStatus.Error)
                                              //{
                                              //    Debug.WriteLine(result.ErrorMessage);
                                              //}
                                              //else if (result.Status == PayPalStatus.Successful)
                                              //{
                                              //    Debug.WriteLine(result.ServerResponse.Response.Id);
                                              //}
            #endregion
            #region Shipping Address (Optional)            // Shipping Address(Optional)
                                                            // Optional shipping address parameter into Buy methods.
                                                            //var resultShippingAddress = await CrossPayPalManager.Current.Buy(
                                                            //            new PayPalItem(
                                                            //                "Test Product",
                                                            //                new Decimal(12.50), "USD"),
                                                            //                new Decimal(0),
                                                            //                new ShippingAddress("My Custom Recipient Name", "Custom Line 1", "", "My City", "My State", "12345", "MX")
                                                            //           );
                                                            //if (result.Status == PayPalStatus.Cancelled)
                                                            //{
                                                            //    Debug.WriteLine("Cancelled");
                                                            //}
                                                            //else if (result.Status == PayPalStatus.Error)
                                                            //{
                                                            //    Debug.WriteLine(result.ErrorMessage);
                                                            //}
                                                            //else if (result.Status == PayPalStatus.Successful)
                                                            //{
                                                            //    Debug.WriteLine(result.ServerResponse.Response.Id);
                                                            //}
            #endregion
            #region Future Payments            //var result = await CrossPayPalManager.Current.RequestFuturePayments();
                                                //if (result.Status == PayPalStatus.Cancelled)
                                                //{
                                                //    Debug.WriteLine("Cancelled");
                                                //}
                                                //else if (result.Status == PayPalStatus.Error)
                                                //{
                                                //    Debug.WriteLine(result.ErrorMessage);
                                                //}
                                                //else if (result.Status == PayPalStatus.Successful)
                                                //{
                                                //    //Print Authorization Code
                                                //    Debug.WriteLine(result.ServerResponse.Response.Code);
                                                //}
            #endregion
            #region Profile sharing            //var result = await CrossPayPalManager.Current.AuthorizeProfileSharing();
                                                //if (result.Status == PayPalStatus.Cancelled)
                                                //{
                                                //    Debug.WriteLine("Cancelled");
                                                //}
                                                //else if (result.Status == PayPalStatus.Error)
                                                //{
                                                //    Debug.WriteLine(result.ErrorMessage);
                                                //}
                                                //else if (result.Status == PayPalStatus.Successful)
                                                //{
                                                //    Debug.WriteLine(result.ServerResponse.Response.Code);
                                                //}

            #endregion
            #region Obtain a Client Metadata ID            //Print Client Metadata Id
                                                            //Debug.WriteLine(CrossPayPalManager.Current.ClientMetadataId);
            #endregion        }

        private void BrowserUrl(object sender, EventArgs e)
        {
            MerchantParams merchantParams = new MerchantParams();
            var serializeObject = JsonConvert.SerializeObject(merchantParams);
            merchantParams.amount = TotalPrice.Text;
            merchantParams.dateTime = DateTime.Now.ToString();
            
            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);
            string plain = "{\"merchantId\":\"M00006063\",\"apiKey\":\"Jt5cO5cf2jg8bX4Bc9yw0Nr8Ng5zm5xz\",\"txnId\":\""+myRandomNo+"\",\"amount\":\"10.00\",\"dateTime\":\"2023-08-02 08:44:47\",\"custMail\":\"test@test.com\",\"custMobile\":\"9876543210\",\"udf1\":\"NA\",\"udf2\":\"NA\",\"returnURL\":\"https://mewanuts.shooppy.in/\",\"isMultiSettlement\":\"0\",\"productId\":\"DEFAULT\",\"channelId\":\"0\",\"txnType\":\"DIRECT\",\"udf3\":\"NA\",\"udf4\":\"NA\",\"udf5\":\"NA\",\"instrumentId\":\"NA\",\"cardDetails\":\"NA\",\"cardType\":\"NA\",\"ResellerTxnId\":\"NA\",\"Rid\":\"R0000259\"}";
            DataService dataService = new DataService();
            var Encrypt = dataService.EncryptPaymentRequest("M00006063", "Jt5cO5cf2jg8bX4Bc9yw0Nr8Ng5zm5xz", plain);

            Content = new StackLayout
            {

                Children =
                     {
                               

                      new MyWebview()
                      {
                      
                          url="https://pa-preprod.1pay.in/payment/payprocessorV2",
                            WidthRequest = 300,
                            HeightRequest = 1100,
                            data = string.Format("reqData={0}&merchantId={1}", Encrypt, "M00006063")
                      },
                     },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand


            };
            
        }
        
    }}