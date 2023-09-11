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
using Xamarin.Forms.Extended;
using DellyShopApp.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DellyShopApp
{

    public partial class OrgPage
    {
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int Org_CategoryId = Convert.ToInt32(SecureStorage.GetAsync(" Org_CategoryId").Result);
        private const int PageSize = 20;
        public bool IsWorking
        {
            get; set;
        }
        public InfiniteScrollCollection<Tbl_ShopModel> Items { get; set; }


        public OrgPage()
        {

        }

        public OrgPage(int oId)
        {
            Org_CategoryId = oId;
            GetDeviceInfo();
            InitializeComponent();
        }
        
        private async void InittMainPage()
        {
            Busy();
            this.BindingContext = this;
            var listOrgData = await GetProducts(0, 20);
            if (listOrgData.Count() == 0)
            {
                listOrgData = await DataService.GetAllOrganizations(Org_CategoryId); //DataService.Instance.ShopDetails;
                await App.SQLiteDb.SaveOrganizationAsync(listOrgData);
            }
            Items = new InfiniteScrollCollection<Tbl_ShopModel>
            {
                OnLoadMore = async () =>
                {
                    // load the next page
                    var page = Items.Count / PageSize;
                    var items = await GetProducts(page, PageSize); //await DataItems.GetItemsAsync(page, PageSize);
                    IsWorking = false;
                    return items;
                }
            };
            await loadDataAsync();
            NotBusy();
            //shop.ItemsSource = DataService.Instance.ShopDetails;
        }
        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            MainLayout.Opacity = 0.7;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ChechConnectivity())
            {
                InittMainPage();
            }
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            MainLayout.Opacity = 100;
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

        private void TapGestureRecognizer_Tapped(string orgId, string Img)
        {
            
            var neworgId = Convert.ToInt32(orgId);
            if (neworgId != oldorgId)
            {
                Xamarin.Essentials.SecureStorage.RemoveAll();
            }
            SecureStorage.SetAsync("OrgId", orgId);
            SecureStorage.SetAsync("ImgId", Img);
            Navigation.PushAsync(new HomeTabbedPage());
        }
        private string GetDeviceInfo()
        {
            string mac = string.Empty;
            string ip = string.Empty;
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||      
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    var address = netInterface.GetPhysicalAddress();
                    mac = BitConverter.ToString(address.GetAddressBytes());

                    IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
                    if (addresses != null && addresses[0] != null)
                    {
                        ip = addresses[0].ToString();
                        break;
                    }
                }
            }
            return mac;
        }

        private void BackPage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public async Task<List<Tbl_ShopModel>> GetProducts(int pageIndex = 0, int pageSize = 20)
        {
            return await App.SQLiteDb.GetOrgAsync(pageIndex, pageSize);
        }
        private async Task loadDataAsync()
        {
            var items = await GetProducts(pageIndex: 0, pageSize: PageSize);

            Items.AddRange(items);
            OrgList.ItemsSource = Items;

        }
    }
}