using System;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;
using DellyShopApp.Services;
using Xamarin.Essentials;
using Android.OS;
using Android.Runtime;
using Acr.UserDialogs;
using static DellyShopApp.Views.ListViewData;
using System.Net.NetworkInformation;
using System.Net;
using Plugin.Connectivity;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DellyShopApp.Helpers;

namespace DellyShopApp
{

    public partial class MainPage
    {
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public string Banner = "Mobile_Landscape_Banner.png" ;
        public string CurrentAddress = "";
        CancellationTokenSource cts;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (ChechConnectivity())
            {
                Navigation.PushAsync(new HomeTabbedPage());
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

    

    }
}