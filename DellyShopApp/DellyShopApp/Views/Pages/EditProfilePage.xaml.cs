using DellyShopApp.Services;
using Plugin.Connectivity;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (UserName.Text == null || UserName.Text == "")
            {
                await DisplayAlert("o" +
                    "pps..", "Please Enter Your UserName", "Ok");
                return;
            }
            else if (EmailAddress.Text == null || EmailAddress.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your EmailAddress", "Ok");
                return;
            }

            else if (number.Text == null || number.Text == "" || number.Text.Length < 10)
            {
                await DisplayAlert("opps..", "Please Enter Your Phonenumber", "Ok");
                return;
            }
            else if (address.Text == null || address.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your Address", "Ok");
                return;
            }
            else if (statename.Text == null || statename.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your State Name", "Ok");
                return;
            }
            else if (cityname.Text == null || cityname.Text == "")
            {
                await DisplayAlert("opps..", "Please Enter Your City", "Ok");
                return;
            }
            else if (zipcode.Text == null || zipcode.Text == "" || zipcode.Text.Length < 6)
            {
                await DisplayAlert("opps..", "Please Enter Your ZipCode", "Ok");
                return;
            }
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            bool EmailCheck = regex.IsMatch(EmailAddress.Text.Trim());
            if (!EmailCheck)
            {
                await DisplayAlert("opps..", "Please Enter Valid Email Address", "Ok");
                return;
            }
            else
            {
                var Page = new Page2(changeUserData);
                _ = DisplayAlert("Yes", "Your Profile Update Successfully", "Okay");
                await DataService.EditUserData(changeUserData);
                await Navigation.PushAsync(new HomeTabbedPage());
            }
        }
        void BackButton(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
