using DellyShopApp.Models;
using DellyShopApp.Services;using System;using System.Collections.Generic;using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;using static DellyShopApp.Views.Pages.HomeTabbedPage;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class EditProfilePage    {
        private readonly Registration _registration;        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        public static string Text = string.Empty;        public EditProfilePage()        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();
            InittEditProfilepage();            
            //int OrgId = DataService.Instance.EditProfile.OrgId;

        }
        private async void InittEditProfilepage()
        {
            var getuserData = await DataService.GetUserById(userId, orgId);
            var getUser = getuserData.FirstOrDefault();
            UserName.Text = getUser.ChangeUsername;            EmailAddress.Text =  getUser.ChangeEmail;
            number.Text = getUser.ChangeContactNumber;            address.Text =  getUser.ChangeAddress;            statename.Text =  getUser.ChangeState;            cityname.Text =  getUser.Changecity;            zipcode.Text =  getUser.ChangeZipcode;        }
        protected override void OnAppearing()        {            base.OnAppearing();        }        private async void Button_Clicked(System.Object sender, System.EventArgs e)   {


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

            var Page = new Page2(changeUserData);            _ = DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");            await DataService.EditUserData(changeUserData);            await Navigation.PushAsync(new HomeTabbedPage());
        }        void BackButton(System.Object sender, System.EventArgs e)        {            Navigation.PopAsync();        }

    }}