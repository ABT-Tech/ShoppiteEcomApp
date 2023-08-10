using System;using System.Collections.Generic;using DellyShopApp.Models;using DellyShopApp.Services;using Plugin.Connectivity;
using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class Canselpage    {        private List<Order> _procutListModel = new List<Order>();        public List<ProductListModel> Product { get; }        public Canselpage()        {            InitializeComponent();
            if (ChechConnectivity())
            {
                InittSuccessPage();
            }                                       }
        private bool ChechConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            else
            {
                DisplayAlert("Opps!", "Please Check Your Internet Connection", "ok");
                return false;
            }
        }               private async void InittSuccessPage()        {
            //BasketItems.ItemsSource = DataService.Instance.ProcutListModel;
        }        private async void ContinueClick(object sender, EventArgs e)        {
            //.Current.MainPage = new HomeTabbedPage();
            Application.Current.MainPage = new NavigationPage(new HomeTabbedPage());        }    }}