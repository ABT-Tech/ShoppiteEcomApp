using System;using DellyShopApp.Models;using DellyShopApp.Services;using DellyShopApp.Views.CustomView;using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;using Xamarin.Forms.Xaml;namespace DellyShopApp.Views.Pages{    [XamlCompilation(XamlCompilationOptions.Compile)]    public partial class LastViewPage    {
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
            LastViewList.ItemsSource = await DataService.GetAllProductsByOrganizations(orgId);//DataService.Instance.ProcutListModel;
            NotBusy();        }        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            MainLayout.Opacity = 0.7;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            MainLayout.Opacity = 100;
        }        private async void ClickItem(object sender, EventArgs e)        {            if (!(sender is PancakeView pancake)) return;            if (!(pancake.BindingContext is ProductListModel item)) return;            await Navigation.PushAsync(new ProductDetail(item));        }    }}