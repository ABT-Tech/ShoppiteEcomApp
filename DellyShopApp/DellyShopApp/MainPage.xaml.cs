using System;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;
using DellyShopApp.Services;
using Xamarin.Essentials;
using Android.OS;
using Android.Runtime;
using Acr.UserDialogs;
using System.Net.NetworkInformation;
using System.Net;
using Plugin.Connectivity;
using System.Threading.Tasks;

namespace DellyShopApp
{

    public partial class MainPage
    {
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public Command TouchCommand { get; }
        public MainPage()
        {
            GetDeviceInfo();
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittMainPage();
            }
        }
        
        private async void InittMainPage()
        {
           
            this.BindingContext = this;
            var AllOrganizations = await DataService.GetAllOrganizations(); //DataService.Instance.ShopDetails;
            float rows = (float)AllOrganizations.Count / 2;
            double rowcount = Math.Round(rows);
            if (AllOrganizations.Count % 2 == 1)
            {
                rowcount = rowcount + 1;
            }
            var productIndex = 0;
            for (int rowIndex = 0; rowIndex < rowcount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                {
                    if (productIndex >= AllOrganizations.Count)
                    {
                        break;
                    }
                    var product = AllOrganizations[productIndex];
                    productIndex += 1;
                    var label = new Label
                    {
                        Text = product.ShopName,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Chocolate,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                    };
                    var image = new Image
                    {
                        Source = product.Image,
                        BackgroundColor = Color.WhiteSmoke,
                        Margin = 15,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = 200,
                        WidthRequest = 200
                    };
                    var Orglabel = new Label
                    {
                        Text = product.OrgId.ToString(),
                        IsVisible = false
                    };
                    image.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text, product.Image.ToString()
                       /* product.Image = Xamarin.CommunityToolkit.Effects.TouchEffect.GetPressedScale*/)),


                    }) ;
                    //image.GestureRecognizers.Add(new TapGestureRecognizer 
                    //{
                    //    Command = new Command(() => TapGestureRecognizer_Tapped(Xamarin.CommunityToolkit.Effects.TouchEffect.GetNativeAnimation.product.Image))
                    //});
                    shop.Children.Add(image, columnIndex, rowIndex);
                    shop.Children.Add(label, columnIndex, rowIndex);
                    shop.Children.Add(Orglabel, columnIndex, rowIndex);
                }
            }
            
            //shop.ItemsSource = DataService.Instance.ShopDetails;
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
           // StartAnimation();
        }
        //private async void StartAnimation()
        //{
        //    await Task.Delay(200);
        //    await Imagebtn.FadeTo(0, 250);
        //    await Task.Delay(200);
        //    await Imagebtn.FadeTo(1, 250);
        //}
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

    }
}