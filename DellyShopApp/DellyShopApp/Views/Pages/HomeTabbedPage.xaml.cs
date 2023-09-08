using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using static DellyShopApp.Views.TabbedPages.BasketPage;using DellyShopApp.Models;
using Xamarin.Essentials;
using DellyShopApp.Views.TabbedPages;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomeTabbedPage : TabbedPage    {        private Page2 page;
                public HomeTabbedPage()        {               InitializeComponent();
           
        }
        protected async override void OnAppearing()
        {
            var AllProducts = await DataService.GetAllProductsByOrganizations(0, null, 4);
            await App.SQLiteDb.SaveItemAsync(AllProducts);
            
            var homePage = new HomePage();
            homePage.IconImageSource = "Home";
            this.Children.Add(homePage);

            var categoryPage = new CategoryPage();
            categoryPage.IconImageSource = "Category";
            this.Children.Add(categoryPage);

            var myCartPage1 = new MyCartPage();
            myCartPage1.IconImageSource = "Shops";
            this.Children.Add(myCartPage1);

            var myCartPage = new MyCartPage();
            myCartPage.IconImageSource = "Offer";
            this.Children.Add(myCartPage);

            var profilePage = new ProfilePage();
            profilePage.IconImageSource = "Profile";
            this.Children.Add(profilePage);
        }
        public HomeTabbedPage(Page2 page)        {            this.page = page;            
        }        public partial class Page2 : ContentPage        {            public ChangeUserData model;            public Page2(ChangeUserData m)            {                this.model = m;            }
        }    }}