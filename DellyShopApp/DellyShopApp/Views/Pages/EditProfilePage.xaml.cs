using DellyShopApp.Models;
using DellyShopApp.Services;using System;using System.Collections.Generic;using Xamarin.Essentials;
using Xamarin.Forms;using Xamarin.Forms.Xaml;using static DellyShopApp.Views.Pages.HomeTabbedPage;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class EditProfilePage    {
        private readonly Registration _registration;        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public static string Text = string.Empty;        public EditProfilePage()        {            InitializeComponent();            int OrgId = DataService.Instance.EditProfile.OrgId;            UserName.Text = DataService.Instance.EditProfile.Username;            EmailAddress.Text = DataService.Instance.EditProfile.Email;            address.Text = DataService.Instance.EditProfile.Address;            statename.Text = DataService.Instance.EditProfile.State;            cityname.Text = DataService.Instance.EditProfile.city;            zipcode.Text = DataService.Instance.EditProfile.Zipcode;            number.Text = DataService.Instance.EditProfile.ContactNumber;
         }
        protected override void OnAppearing()        {            base.OnAppearing();        }        private async void Button_Clicked(System.Object sender, System.EventArgs e)        {                        //var changeUserData = new ChangeUserData            //{            //    Username = UserName.Text,
            //    Email = EmailAddress.Text,
            //    Address = address.Text,
            //    State = statename.Text,
            //    city = cityname.Text,
            //    Zipcode = zipcode.Text,
            //    ContactNumber = number.Text,
            //    OrgId = orgId,            //};
          
            //await DataService.ChangeUserData(changeUserData);            //var Page = new Page2(changeUserData);            //_ = DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");            await Navigation.PushAsync(new HomeTabbedPage());

        }        void BackButton(System.Object sender, System.EventArgs e)        {            Navigation.PopAsync();        }

    }}