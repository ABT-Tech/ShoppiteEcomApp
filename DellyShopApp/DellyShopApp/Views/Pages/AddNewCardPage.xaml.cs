using System;
using System.ComponentModel;
using System.Diagnostics;
using DellyShopApp.ViewModel;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
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
            var result = await CrossPayPalManager.Current.ScanCard(CardIOLogo.None);
            if (result.Status == PayPalStatus.Cancelled)
            {
                Debug.WriteLine("Cancelled");
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                if (result.Card.CardImage != null)
                {
                    var Source = result.Card.CardImage;
                }

                CardNumber.Text = result.Card.CardNumber;
                CardExpirationDate.Text = $"{result.Card.ExpiryMonth}/{result.Card.ExpiryYear}";
                CvvEntry.Text = result.Card.Cvv;
                Debug.WriteLine($"CardNumber: {result.Card.CardNumber}, CardType: {result.Card.CardType.ToString()}, Cvv: {result.Card.Cvv}, ExpiryMonth: {result.Card.ExpiryMonth}");
                Debug.WriteLine($"ExpiryYear: {result.Card.ExpiryYear}, PostalCode: {result.Card.PostalCode}, RedactedCardNumber: {result.Card.RedactedCardNumber}, Scaned: {result.Card.Scaned}");
            }
        }
    }

}