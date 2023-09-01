using System;using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.Pages.Base;using Xamarin.Essentials;using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class VerificationPage    {
        public string FirebaseToken = SecureStorage.GetAsync("FirebaseToken").Result;
        public string macId = SecureStorage.GetAsync("MacId").Result;        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public VerificationPage()        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);            InitializeComponent();        }        private async void LoginButtonClick(object sender, EventArgs e)        {
            if (email.Text == null || email.Text == "")            {                await DisplayAlert("Opps", "Please Enter Email", "Ok");                return;            }            else if (num.Text == null || num.Text == "" || num.Text.Length < 10)            {                await DisplayAlert("Opps", "Please Enter Phonenumber", "Ok");                return;            }
            var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");            if (result == true)            {
                await Navigation.PushAsync(new HomeTabbedPage());            }            else            {                await Navigation.PushAsync(new RegisterPage());            }
            //var userDetail =  await DataService.Login(login);

            // await Navigation.PushAsync(new LoginPage());
        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }        private async void ForgetPassClick(object sender, EventArgs e)        {            await Navigation.PushAsync(new ForgetPasswordPage());
        }        private async void Signup(object sender, EventArgs e)        {
            await Navigation.PushAsync(new RegisterPage());        }


    }}