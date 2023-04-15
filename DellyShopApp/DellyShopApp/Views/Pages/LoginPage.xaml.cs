using System;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using Xamarin.Essentials;
using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class LoginPage    {
        
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public LoginPage()        {            InitializeComponent();        }        private async void LoginButtonClick(object sender, EventArgs e)        {            var login = new Login            {                email = email.Text,                password = pswd.Text,                org_Id = orgId,
                type = "Client"
            };
            if (email.Text == null || email.Text == "")            {                await DisplayAlert("Opps", "Please Enter Email", "ok");                return;            }            else if (pswd.Text == null || pswd.Text == "")            {                await DisplayAlert("Opps", "Please Enter Password", "ok");                return;            }

            var userDetail = await DataService.Login(login);
            if(userDetail != null)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("UserId", userDetail.UserId.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Tokan", userDetail.jwt_token.ToString());
                await DisplayAlert("Yes", "You Have a Login Successfully", "Ok");
            }
            else
            {
                await DisplayAlert("Opps", "Invalid Data","Ok");
                return;
            }


            await Navigation.PushAsync(new HomeTabbedPage());        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }        private async void ForgetPassClick(object sender, EventArgs e)        {            string result = await DisplayPromptAsync(AppResources.ForgotPass,                AppResources.EnterEmailAddress);            if (string.IsNullOrEmpty(result)) return;            await DisplayAlert(AppResources.Success,                AppResources.SuccessSendEmail                + " " + result, AppResources.Okay);        }

       

      private async  void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

     private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new VenderLoginPage());
        }
    }}