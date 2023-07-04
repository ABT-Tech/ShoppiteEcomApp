using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using static DellyShopApp.Views.TabbedPages.BasketPage;using DellyShopApp.Models;
using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomeTabbedPage : TabbedPage    {
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int userId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);        private Page2 page;        public HomeTabbedPage()        {            InitializeComponent();
            InittHomeTabbedPage();
        }
        private async void InittHomeTabbedPage()
        {
            badge.BindingContext = await DataService.GetNumOfItemsInCart(orgId,userId);
        }
        public HomeTabbedPage(Page2 page)        {            this.page = page;
            
        }        public partial class Page2 : ContentPage        {            public ChangeUserData model;            public Page2(ChangeUserData m)            {                this.model = m;                           }
        }    }}