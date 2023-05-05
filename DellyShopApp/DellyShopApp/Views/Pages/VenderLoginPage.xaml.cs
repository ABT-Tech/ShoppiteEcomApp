using System;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.TabbedPages;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class VenderLoginPage    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public VenderLoginPage()        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();        }        private async void LoginButtonClick(object sender, EventArgs e)        {
            var login = new Login            {                email = email.Text,                password = pswd.Text,                org_Id = orgId,
                type = "vendor"
            };
            if (email.Text == null || email.Text == "")            {                await DisplayAlert("Opps", "Please Enter Email", "ok");                return;            }            else if (pswd.Text == null || pswd.Text == "")            {                await DisplayAlert("Opps", "Please Enter Password", "ok");                return;            }
            var userDetail = await DataService.Login(login);
            if (userDetail != null)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("UserId", userDetail.UserId.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Tokan", userDetail.jwt_token.ToString());
               
            }
            else
            {
                await DisplayAlert("Opps", "Invalid Data", "Ok");
                return;
            }


               await Navigation.PushAsync(new VendorsFirstPage());



        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }        private async void ForgetPassClick(object sender, EventArgs e)        {            string result = await DisplayPromptAsync(AppResources.ForgotPass,                AppResources.EnterEmailAddress);            if (string.IsNullOrEmpty(result)) return;            await DisplayAlert(AppResources.Success,                AppResources.SuccessSendEmail                + " " + result, AppResources.Okay);        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }





    }
    }