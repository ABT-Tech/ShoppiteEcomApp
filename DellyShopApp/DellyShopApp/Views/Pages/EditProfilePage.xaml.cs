using DellyShopApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DellyShopApp.Views.Pages.HomeTabbedPage;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage
    {
        public static string Text = string.Empty;
        public EditProfilePage()
        {
            InitializeComponent();

            int UserId = DataService.Instance.EditProfile.UserId;
            UserName.Text = DataService.Instance.EditProfile.ChangeName;
            EmailAddress.Text = DataService.Instance.EditProfile.ChangeEmail;
            PhoneNumber.Text = DataService.Instance.EditProfile.ChangePhoneNumber;
            Address.Text = DataService.Instance.EditProfile.ChangeAddress;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var changeUserData = new ChangeUserData
            {

                
                ChangeName = UserName.Text,
                ChangeEmail = EmailAddress.Text,
                ChangePhoneNumber = PhoneNumber.Text,
                ChangeAddress = Address.Text,
            };
            var Page = new Page2(changeUserData);
            _ = DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");
            await Navigation.PushAsync(new HomeTabbedPage());
           
        }

        void BackButton(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
      
    }
}
