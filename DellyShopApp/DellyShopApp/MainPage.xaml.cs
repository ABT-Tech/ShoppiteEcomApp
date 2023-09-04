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
            GetDeviceInfo();
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittMainPage();
            }
        }
        
        private async void InittMainPage()
        {
            Busy();
            this.BindingContext = this;
            CarouselView.ItemsSource = DataService.Instance.Carousel;
            var AllOrganizations = await DataService.GetAllOrganizationCategories(); //DataService.Instance.ShopDetails;
          
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
                        Text = product.CategoryName,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Chocolate,
                        MaxLines = 1,
                        LineBreakMode = LineBreakMode.TailTruncation,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,

                    };                    
                        var image = new Image
                        {
                            Aspect = Aspect.Fill,
                            Source = product.CategoryImage,
                            BackgroundColor = Color.WhiteSmoke,
                            Margin = new Thickness(5, 0, 5, 10),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 100,
                            WidthRequest = 300,
                        };
                        var Orglabel = new Label
                        {
                            Text = product.Org_CategoryId.ToString(),
                            IsVisible = false
                        };
                        image.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text, product.CategoryImage.ToString())),
                        });
                        shop.Children.Add(image, columnIndex, rowIndex);

                        shop.Children.Add(Orglabel, columnIndex, rowIndex);
                    }
                }
            
            NotBusy();
            //shop.ItemsSource = DataService.Instance.ShopDetails;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(20));
            CancellationTokenSource cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync();

            if (location != null)
                await GetLocation(location);
            else
                await DisplayAlert("Unknown", "Your Location is unknown", "Ok");
        }
        public void Busy()
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
            
            //var neworgId = Convert.ToInt32(orgId);
            //if (neworgId != oldorgId)
            //{
            //    Xamarin.Essentials.SecureStorage.RemoveAll();
            //}
            SecureStorage.SetAsync("OrgCatId", orgId);
            //SecureStorage.SetAsync("ImgId", Img);
            var oId = Convert.ToInt32(orgId);
            Navigation.PushAsync(new OrgPage(oId));
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
            SecureStorage.SetAsync("DeviceId", mac);

        }

        public async Task GetLocation(Location location)
        {
            try
            {   
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    var geocodeAddress = $"{ placemark.Thoroughfare}" + " " + //Address
                                         $"{ placemark.SubLocality}" + " " + //Address area name

                    $"{placemark.Locality} {placemark.SubAdminArea}"; //CityName;
                    if (geocodeAddress.Length > 25)
                        geocodeAddress = geocodeAddress.Substring(0, 20)+"...";
                    AddressLabel.Text =  geocodeAddress;
                    CurrentAddress += geocodeAddress;
                }
                else
                    await DisplayAlert("Error occurred", "Unable to retreive address information", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error occurred", ex.Message.ToString(), "Ok");
            }
        }


    }
}