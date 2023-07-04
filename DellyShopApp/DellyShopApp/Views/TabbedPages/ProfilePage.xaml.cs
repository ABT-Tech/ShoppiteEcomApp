using Android.Views;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Views.View;

namespace DellyShopApp.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage
    {
        public string userAuth = SecureStorage.GetAsync("Usertype").Result;
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public object Element { get; private set; }
        public ProfilePage()
        {
            InitializeComponent();
            if (userId == 0 || userAuth != "Client")
            {
                EditProfile.IsVisible = false;
                MyOder.IsVisible = false;
                MyOder1.IsVisible = false;
                MyFav.IsVisible = false;
                LastView.IsVisible = false;
                Logout.IsVisible = false;
                Login.IsVisible = true;
                Vendorpcake.IsVisible = true;
                Vendorlbl.IsVisible = true;
                Earnlbl.IsVisible = true;
                Earnpcake.IsVisible = true;
                Fbacklbl.IsVisible = true;
                Fbackpcake.IsVisible = true;
            }
            else
            {
                EditProfile.IsVisible = true;
                MyOder.IsVisible = true;
                MyOder1.IsVisible = true;
                MyFav.IsVisible = true;
                LastView.IsVisible = true;
                Logout.IsVisible = true;
                Login.IsVisible = false;
                Vendorpcake.IsVisible = false;
                Vendorlbl.IsVisible = false;
                Earnlbl.IsVisible = false;
                Earnpcake.IsVisible = false;
                Fbacklbl.IsVisible = false;
                Fbackpcake.IsVisible = false;
            }
        }

        private void OrderInfoClick(object sender, EventArgs e)
        {
            if (!(sender is PancakeView stack)) return;
            switch (stack.ClassId)
            {
                case "EditProfile":
                    OpenPage(new EditProfilePage());
                    break;

                case "MyOder":
                    OpenPage(new MyOrderPage());
                    break;

                case "MyFav":
                    OpenPage(new MyFavoritePage());
                    break;

                case "LastView":
                    OpenPage(new LastViewPage());
                    break;

                case "MyComments":
                    OpenPage(new MyCommentsPage());
                    break;

                case "Notifications":
                    OpenPage(new NotificationPage());
                    break;

                case "Settings":
                    OpenPage(new SettingsPage());
                    break;
            }
        }

        private void OpenPage(Page page)
        {
            Navigation.PushAsync(page);
        }
        protected void LogOutClick(object sender, EventArgs args)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            Xamarin.Essentials.SecureStorage.RemoveAll();
        }

        protected void LogInClick(object sender, EventArgs args)
        {
            OpenPage(new LoginPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VendorLoginPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new HomeTabbedPage());
        }

        private void Registration(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        
        private void BrowserUrl(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VendorRegisterPage());
            //var url = "https://shooppy.in/";
            //Device.OpenUri(new Uri(url));
        }
    }
}