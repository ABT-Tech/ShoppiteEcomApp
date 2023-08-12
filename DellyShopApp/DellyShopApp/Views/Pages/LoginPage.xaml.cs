using System;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.Pages.Base;
using FirebaseAdmin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class LoginPage    {        
        public string FirebaseToken = SecureStorage.GetAsync("FirebaseToken").Result; 
        public string macId = SecureStorage.GetAsync("MacId").Result;
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public LoginPage()        {
           Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();        }        private async void LoginButtonClick(object sender, EventArgs e)        {            var login = new Login            {                email = email.Text,                password = pswd.Text,                org_Id = orgId,
                type = "Client",
               
            };
            if(email.Text == null || email.Text == "")
            {
                await DisplayAlert("Opps", "Please Enter Email", "ok");
                return;
            }
            else if (pswd.Text == null || pswd.Text == "")
            {
                await DisplayAlert("Opps", "Please Enter Password", "ok");
                return;
            }

            var userDetail =  await DataService.Login(login);            if (userDetail != null)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("UserId", userDetail.UserId.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Token", userDetail.jwt_token.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Usertype", "Client");

                var token = new Models.FirebaseToken
                {
                    Token = FirebaseToken,
                    UserID = userDetail.UserId,
                    MacID = macId
                };
                // await DataService.UpdateFireBaseToken(token);
                // FirebaseToken firebase = new Models.FirebaseToken();
                // await DataService.UpdateFireBaseToken(firebase);
            }
            else
            {
               await DisplayAlert("Opps", "InValid Username or Password", "ok");
                return;
            }                 
             
            await Navigation.PushAsync(new HomeTabbedPage());        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }        private async void ForgetPassClick(object sender, EventArgs e)        {
            await Navigation.PushAsync(new ForgetPasswordPage());            
        }

        private async void Signup(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new RegisterPage());
        }

      
    }}