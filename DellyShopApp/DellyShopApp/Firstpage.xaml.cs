using System;
using DellyShopApp.Languages;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;

namespace DellyShopApp
{
    public partial class FirstPage
    {
        public FirstPage()
        {
            InitializeComponent();

        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void LogIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

    }
}