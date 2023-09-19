using System;using System.Threading.Tasks;
using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;using DellyShopApp.Animations;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class LoginPage    {
        
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public LoginPage()        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();        }        protected override void OnAppearing()        {            base.OnAppearing();            Task.Run(async () =>            {
                //await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(MainStack1);                await ViewAnimations.FadeAnimY(MainStack);            });        }        private async void LoginButtonClick(object sender, EventArgs e)        {            var login = new Login            {                email = email.Text,                password = pswd.Text,                org_Id = orgId,
                type = "Client"
            };
            if (email.Text == null || email.Text == "")            {                await DisplayAlert("Opps", "Please Enter Email", "Ok");                return;            }            else if (pswd.Text == null || pswd.Text == "")            {                await DisplayAlert("Opps", "Please Enter Password", "Ok");                return;            }

            var userDetail = await DataService.Login(login);
            if(userDetail != null)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("UserId", userDetail.UserId.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Tokan", userDetail.jwt_token.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Usertype", "Client");

            }
            else
            {
                await DisplayAlert("Opps", "Invalid Data","Ok");
                return;
            }


            await Navigation.PushAsync(new HomeTabbedPage());        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }        private async void ForgetPassClick(object sender, EventArgs e)        {
            //string result = await DisplayPromptAsync(AppResources.ForgotPass,
            //    AppResources.EnterEmailAddress,AppResources.Pass);
            //if (string.IsNullOrEmpty(result)) return;
            //await DisplayAlert(AppResources.Success,
            //    AppResources.SuccessSendEmail
            //    + " " + result, AppResources.Okay);
            await Navigation.PushAsync(new ForgetPasswordPage());        }

       

      private async  void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

     private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new VenderLoginPage());
        }

        
    }}