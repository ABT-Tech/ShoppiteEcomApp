using DellyShopApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;




namespace DellyShopApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage
    {
        public static string Text = string.Empty;
        public EditProfilePage()
        {
            InitializeComponent();
           
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
            await Navigation.PushAsync(new HomeTabbedPage());
            DisplayAlert("Yes", "Your Profile Edit Successfully", "Okay");
        }

        void BackButton(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
      
    }
}
