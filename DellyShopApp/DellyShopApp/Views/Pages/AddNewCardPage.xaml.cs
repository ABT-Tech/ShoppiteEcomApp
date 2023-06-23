using System;
using System.ComponentModel;
using System.Diagnostics;
using DellyShopApp.ViewModel;
using Plugin.PayCards;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class AddNewCardPage
    {
        public AddNewCardPage()
        {
            InitializeComponent();
            this.BindingContext = new CreditCardPageViewModel();
        }

        private void SaveClick(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        async void ScanCreditCard(System.Object sender, System.EventArgs e)
        {
            //try
            //{
            //    var result = await CrossPayCards.Current.ScanAsync();
            //    if (result != null)
            //    {
            //        if (!string.IsNullOrEmpty(result.CardNumber))
            //        {
            //            CardNumber.Text = result.CardNumber;
            //        }
            //         if (!string.IsNullOrEmpty(result.ExpirationDate))
            //        {
            //            CardExpirationDate.Text = result.ExpirationDate;
            //        }
            //         if (!string.IsNullOrEmpty(result.HolderName))
            //        {
            //            CvvEntry.Text = result.HolderName;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return;
            //}
            // If you don't want to be dependent on Paypal, you can use the code above.



            //Optional parameter CardIOLogo("PayPal", "CardIO" or "None") for ScanCard method by default "PayPal" is used
            
        }
    }

}