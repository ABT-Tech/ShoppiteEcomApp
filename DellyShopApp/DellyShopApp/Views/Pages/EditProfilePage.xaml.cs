using DellyShopApp.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DellyShopApp.Views.TabbedPages.BasketPage;

namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage
    {
        public static string Text = string.Empty;
        public EditProfilePage()
        {
            InitializeComponent();         

            int userId = DataService.Instance.EditProfile.userId;
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
                var changeAddress = new ChangeUserData
                {
                    ChangeName = UserName.Text,
                    ChangeEmail = EmailAddress.Text,
                    ChangePhoneNumber = PhoneNumber.Text,
                    ChangeAddress = Address.Text,
                };
                var Page = new Page2(changeAddress);

                await Navigation.PushAsync(new HomeTabbedPage());
                await DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");
          
        }

        void BackButton(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
      
    }
}
