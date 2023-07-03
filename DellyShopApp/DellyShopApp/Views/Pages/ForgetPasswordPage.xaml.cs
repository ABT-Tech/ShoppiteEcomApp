using System;using System.Text.RegularExpressions;using System.Threading.Tasks;
using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.Pages.Base;using Xamarin.Essentials;using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;using Xamarin.Forms.Xaml;using XamUIDemo.Animations;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class ForgetPasswordPage    {

        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public ForgetPasswordPage()        {            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);            InitializeComponent();        }
        protected override void OnAppearing()        {            base.OnAppearing();            Task.Run(async () =>            {
                //await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(MainStack);            });        }        private async void Button_Clicked(object sender, EventArgs e)        {            var forgetpass = new ForgetPassword            {                Email = email.Text,                Password = pswd.Text,                OrgId = orgId,                ConfirmPassword = CfmPass.Text

            };            if (email.Text == null || email.Text == "")            {                await DisplayAlert("Opps", "Please Enter Email", "ok");                return;            }            else if (pswd.Text == null || pswd.Text == "")            {                await DisplayAlert("Opps", "Please Enter Password", "ok");                return;            }            var forget = await DataService.ForgetPassword(forgetpass);            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");            bool EmailCheck = regex.IsMatch(email.Text.Trim());            if (!EmailCheck)            {                await DisplayAlert("opps..", "Please Enter Valid Email Address", "Ok");                return;            }            else if (pswd.Text != CfmPass.Text)            {                await DisplayAlert("opps..", "Confirm Password Not Match", "Ok");                return;            }            else            {                if (forget == "Password Changed Successfully!!")                {                    await DisplayAlert("Congrulations", forget, "Ok");                    await Navigation.PushAsync(new LoginPage());                }                else                {                    await DisplayAlert("Sorry", forget, "Ok");                    return;                }            }


        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }


        private async void Signup(object sender, EventArgs e)        {
            await Navigation.PushAsync(new RegisterPage());        }


    }}