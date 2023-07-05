using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using static DellyShopApp.Views.TabbedPages.BasketPage;using Xamarin.Essentials;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomeTabbedPage : TabbedPage    {        private Page2 page;        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("userId").Result);
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public HomeTabbedPage()        {            InitializeComponent();
            InittProductDetail();
        }
        private async void InittProductDetail()
        {
            badge.BindingContext = await DataService.GetNumOfItemsInCart(orgId,UserId);

        }        
        public HomeTabbedPage(Page2 page)        {
                       this.page = page;        }        public partial class Page2 : ContentPage        {            public ChangeUserData model;            public Page2(ChangeUserData m)            {                this.model = m;            }


        }    }}