using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.ViewModel;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.ModalPages;
using DellyShopApp.Views.Pages;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage
    {

        private readonly BasketPageVm _basketVm = new BasketPageVm();

        public BasketPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
           
            _basketVm.ProcutListModel = DataService.Instance.BasketModel;
            BasketItems.ItemsSource = _basketVm.ProcutListModel;
            foreach (var item in DataService.Instance.BasketModel)
            {
                DataService.Instance.BaseTotalPrice += item.Price;
            }
            TotalPrice.Text = $"{ DataService.Instance.BaseTotalPrice + 12}$";
        }

        /// <summary>
        /// Go to Address Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAddressClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddNewAddressPage());
        }

        private async void ContinueClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectCreditCardPage(), true);
        }
        /// <summary>
        /// Delete Visible Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemSwipe(object sender, SwipedEventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is ProductListModel item)
            {
                item.VisibleItemDelete = true;
                VisibleDelete(item.Id);
            }
        }
        /// <summary>
        /// Delete Visible Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndeleteItem(object sender, SwipedEventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (pancake.BindingContext is ProductListModel item)
            {
                item.VisibleItemDelete = false;
                VisibleDelete(item.Id);
            }
        }

        private void VisibleDelete(int id)
        {
            var items = _basketVm.ProcutListModel.Where(x => x.Id != id);
            foreach (var item in items)
            {
                item.VisibleItemDelete = false;
            }
        }

        private async void ClickItem(object sender, EventArgs e)
        {
            if (!(sender is PancakeView pancake)) return;
            if (!(pancake.BindingContext is ProductListModel item)) return;
            await Navigation.PushAsync(new ProductDetail(item));
        }

       async void  ContinueWithPaypal(System.Object sender, System.EventArgs e)
        {
            //Single Item
            var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(12.50), "USD"), new Decimal(0));
            if (result.Status == PayPalStatus.Cancelled)
            {
                Debug.WriteLine("Cancelled");
            }
            else if (result.Status == PayPalStatus.Error)
            {
                Debug.WriteLine(result.ErrorMessage);
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                Debug.WriteLine(result.ServerResponse.Response.Id);
            }

            #region List of Items
            //var resultList = await CrossPayPalManager.Current.Buy(new PayPalItem[] {
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

            #region Shipping Address (Optional)
            //Shipping Address (Optional)
            //Optional shipping address parameter into Buy methods.
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

            #region Future Payments
            //var result = await CrossPayPalManager.Current.RequestFuturePayments();
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

            #region Profile sharing
            //var result = await CrossPayPalManager.Current.AuthorizeProfileSharing();
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

            #region Obtain a Client Metadata ID
            //Print Client Metadata Id
            //Debug.WriteLine(CrossPayPalManager.Current.ClientMetadataId);
            #endregion
        }
    }
}