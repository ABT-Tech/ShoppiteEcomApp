using System;using System.Collections.Generic;
using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using DellyShopApp.Views.TabbedPages;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class LastViewPage    {
        List<ProductListModel> productListModel = new List<ProductListModel>();
        public int orgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);        public LastViewPage()        {            InitializeComponent();
            if (ChechConnectivity())
            {
                InittLastViewPage();

            }
        }
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
        }
        private async void InittLastViewPage()
        {
            Busy();
            LastViewList.ItemsSource = DataService.Instance.ProcutListModel; //await DataService.GetAllProductsByOrganizations(orgId);
            NotBusy();
            foreach (var varient in productListModel)            {                if (varient.SpecificationNames != "")                {                    varient.IsSpecificationNames = true;                }                else
                {
                    varient.IsSpecificationNames = false;                }            }
        }        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;

        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;

        }    }}