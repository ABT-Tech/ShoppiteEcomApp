using System;using System.Threading.Tasks;
using DellyShopApp.Animations;
using DellyShopApp.Languages;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.TabbedPages;
using Xamarin.Essentials;using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class VendorLoginPage    {        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public VendorLoginPage()        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);            InitializeComponent();        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>            {
                //await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(MainStack1);                await ViewAnimations.FadeAnimY(MainStack);            });
        }        private async void LoginButtonClick(object sender, EventArgs e)        {            var login = new Login            {                email = email.Text,                password = pswd.Text,                org_Id = orgId,
                type = "vendor"
            };
            if (email.Text == null || email.Text == "")
            {
                await DisplayAlert("Opps", "Please Enter Email", "ok");
                return;
            }
            else if (pswd.Text == null || pswd.Text == "")
            {
                await DisplayAlert("Opps", "Please Enter Password", "ok");
                return;
            }

            var userDetail = await DataService.Login(login);            if (userDetail != null)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("UserId", userDetail.UserId.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Token", userDetail.jwt_token.ToString());
                await Xamarin.Essentials.SecureStorage.SetAsync("Usertype", "Vendor");

            }
            else
            {
                await DisplayAlert("Opps", "InValid Username or Password", "ok");
                return;
            }
            await Navigation.PushAsync(new VendorsFirstPage());
            await Xamarin.Essentials.SecureStorage.SetAsync("VendorUserId", userDetail.VendorUserId.ToString());

        }        private void BackButton(object sender, EventArgs e)        {            Navigation.PopAsync();        }      
    }}