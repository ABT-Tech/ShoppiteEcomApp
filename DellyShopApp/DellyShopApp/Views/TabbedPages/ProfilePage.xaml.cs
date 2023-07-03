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
                txt.IsVisible = true;
                userimg.IsVisible = true;
                vendorlogin.IsVisible = true;
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
                txt.IsVisible = false;
                userimg.IsVisible = false;
                vendorlogin.IsVisible = false;
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
        

//        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
//        {

//=        }
    }
}