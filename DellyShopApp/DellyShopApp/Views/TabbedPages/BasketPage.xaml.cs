using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.ViewModel;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.ModalPages;using DellyShopApp.Views.Pages;using PayPal.Forms;using PayPal.Forms.Abstractions;using System;using System.Collections.Generic;using System.Diagnostics;using System.Linq;using Xamarin.Essentials;using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.TabbedPages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class BasketPage    {        List<ProductListModel> productListModel = new List<ProductListModel>();        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);               private readonly BasketPageVm _basketVm = new BasketPageVm();        private int _quantity;        private Page2 page;        private List<ProductListModel> Product { get; set; }        public BasketPage(List<ProductListModel> product)        {            this.Product = product;            InitializeComponent();            InittBasketPage();            this.BindingContext = product;        }        public BasketPage(Page2 page)        {            this.page = page;        }        public BasketPage()
        {

        }               public partial class Page2 : ContentPage        {            public ChangeAddress model;            public Page2(ChangeAddress m)            {                this.model = m;            }        }        private async void InittBasketPage()        {            productListModel = this.Product;            BasketItems.ItemsSource = this.Product;//await DataService.GetAllCartDetails(orgId, userId);//DataService.Instance.ProcutListModel;
            AddressPicker.ItemsSource = await DataService.GetAddressByUserId(orgId, userId); //DataService.Instance.changeAddress;
            //productListModel = await DataService.GetAllCartDetails(orgId, userId);
            BasketItems.ItemsSource = productListModel;        }        protected override async void OnAppearing()        {            base.OnAppearing();            this.BindingContext = Product;            BasketItems.ItemsSource = productListModel;//await DataService.GetAllCartDetails(orgId, userId);
           DataService.Instance.TotalPrice = 0;                       foreach (var product in productListModel)            {                DataService.Instance.TotalPrice += product.Quantity * product.Price;                //DataService.Instance.TotalPrice += product.Quantity * product.Price;

                //return;
                //{
                //   DataService.Instance.BaseTotalPrice = product.Quantity * product.Price;
                //}
            }            //SubTotal.Text = $"{ DataService.Instance.BaseTotalPrice }₹";            TotalPrice.Text = $"{ DataService.Instance.TotalPrice}₹";        }










        /// <summary>        /// Go to Address Page        /// </summary>        /// <param name="sender"></param>        /// <param name="e"></param>        private async void AddAddressClick(object sender, EventArgs e)        {            await Navigation.PushModalAsync(new AddNewAddressPage(DataService.Instance.changeAddress.ToList()));        }        private async void ContinueClick(object sender, EventArgs e)        {            OrderCheckOut orderCheckOut = new OrderCheckOut();            orderCheckOut.ProductLists = productListModel;            orderCheckOut.orgid = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);            orderCheckOut.userid = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);            orderCheckOut.Address = (ChangeAddress)AddressPicker.SelectedItem;            orderCheckOut.TotalPrice = ((decimal)DataService.Instance.TotalPrice);            orderCheckOut.OrderGuid = productListModel.FirstOrDefault().OrderGuId;
            if (orderCheckOut.Address == null)            {
                await DisplayAlert("Opps!", "Please Select Address", "ok");                return;            }
            await DataService.Checkout(orderCheckOut);            await Navigation.PushAsync(new SuccessPage(productListModel));        }










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
            #endregion        }    }}