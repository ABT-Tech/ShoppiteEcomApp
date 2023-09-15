using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using static DellyShopApp.Views.TabbedPages.BasketPage;using DellyShopApp.Models;using Xamarin.Essentials;using DellyShopApp.Views.TabbedPages;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomeTabbedPage : TabbedPage    {        private Page2 page;


        public HomeTabbedPage()        {
            InitializeComponent();
            var homePage = new HomePage();            homePage.IconImageSource = "Home";            this.Children.Add(homePage);

            //var categoryPage = new CategoryPage();
            //categoryPage.IconImageSource = "Category";
            //this.Children.Add(categoryPage);
                       var orgPage = new OrgPage(0);            orgPage.IconImageSource = "Shops";            this.Children.Add(orgPage);            var myOfferPage = new MyOfferPage();            myOfferPage.IconImageSource = "Offer";            this.Children.Add(myOfferPage);            var profilePage = new ProfilePage();            profilePage.IconImageSource = "Profile";            this.Children.Add(profilePage);        }        protected override void OnAppearing()        {            base.OnAppearing();        }        public HomeTabbedPage(Page2 page)        {            this.page = page;
        }        public partial class Page2 : ContentPage        {            public ChangeUserData model;            public Page2(ChangeUserData m)            {                this.model = m;            }        }    }}