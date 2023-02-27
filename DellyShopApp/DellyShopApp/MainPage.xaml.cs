using System;
using System.Collections.Generic;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Services;

using Xamarin.Forms;

namespace DellyShopApp
{
    public partial class MainPage 
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            shop.ItemsSource = DataService.Instance.ShopDetails;
            

        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }

        void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }

        void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }

        void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }

        void TapGestureRecognizer_Tapped_4(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }

        void TapGestureRecognizer_Tapped_5(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Views.Pages.HomeTabbedPage());
        }
    }
}