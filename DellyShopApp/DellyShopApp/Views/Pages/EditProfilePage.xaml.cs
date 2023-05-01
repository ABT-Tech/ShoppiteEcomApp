using DellyShopApp.Services;
using Plugin.Connectivity;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using static DellyShopApp.Views.Pages.HomeTabbedPage;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage
    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public static string Text = string.Empty;
        public EditProfilePage()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            InitializeComponent();
            if (ChechConnectivity())
            {
                InittEditProfilePage();
            }
        }
        private bool ChechConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");
                return false;
            }
        }
        private async void InittEditProfilePage()
        {
            var getUserDate = await DataService.GetUserById(userId, orgId);
            var getUser = getUserDate.FirstOrDefault();
            UserName.Text = getUser.ChangeUsername;
            EmailAddress.Text = getUser.ChangeEmail;
            number.Text = getUser.ChangeContactNumber;
            address.Text = getUser.ChangeAddress;
            statename.Text = getUser.ChangeState;
            cityname.Text = getUser.Changecity;
            zipcode.Text = getUser.ChangeZipcode;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var changeUserData = new ChangeUserData
            {
                ChangeUsername = UserName.Text,
                ChangeEmail = EmailAddress.Text,
                ChangeAddress = address.Text,
                ChangeState = statename.Text,
                Changecity = cityname.Text,
                ChangeZipcode = zipcode.Text,
                ChangeContactNumber = number.Text,
                OrgId = orgId,
                UserId = userId
            };
            var Page = new Page2(changeUserData);
            _ = DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");
            await DataService.EditUserData(changeUserData);
            await Navigation.PushAsync(new HomeTabbedPage());
        }
        void BackButton(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
