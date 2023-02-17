using System;
using DellyShopApp.Languages;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButtonClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomeTabbedPage());
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void ForgetPassClick(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync(AppResources.ForgotPass,
                AppResources.EnterEmailAddress);
            if (string.IsNullOrEmpty(result)) return;
            await DisplayAlert(AppResources.Success,
                AppResources.SuccessSendEmail
                + " " + result, AppResources.Okay);
        }
    }
}