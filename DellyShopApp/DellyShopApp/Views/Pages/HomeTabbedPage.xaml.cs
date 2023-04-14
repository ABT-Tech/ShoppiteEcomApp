using System;using Xamarin.Forms;using Xamarin.Forms.Xaml;using DellyShopApp.Services;using static DellyShopApp.Views.TabbedPages.BasketPage;using DellyShopApp.Models;

namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class HomeTabbedPage : TabbedPage    {        private Page2 page;        public HomeTabbedPage()        {                        InitializeComponent();

        }        public HomeTabbedPage(Page2 page)        {            this.page = page;            
        }        public partial class Page2 : ContentPage        {            public ChangeUserData model;            public Page2(ChangeUserData m)            {                this.model = m;            }
        }    }}