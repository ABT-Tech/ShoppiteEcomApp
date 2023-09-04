using System;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;
using DellyShopApp.Services;
using Xamarin.Essentials;
using Acr.UserDialogs;
using static DellyShopApp.Views.ListViewData;
using System.Net.NetworkInformation;
using System.Net;
using Plugin.Connectivity;
using DellyShopApp.Models;

namespace DellyShopApp
{
    
    public partial class MainPage
    {
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public MainPage()
        {
            
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittMainPage();
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
        private async void InittMainPage()
        {
            Busy();
            this.BindingContext = this;
            CarouselView.ItemsSource = DataService.Instance.Carousel;
            CarouselViewa.ItemsSource = DataService.Instance.Carousel;
            var AllOrganizations = await DataService.GetAllOrganizationCategories(); //DataService.Instance.ShopDetails;
            float rows = (float)AllOrganizations.Count / 2;
            double rowcount = Math.Round(rows);
            NotBusy();
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
                        
                        Aspect = Aspect.AspectFit,
                        Source = product.CategoryImage,
                        BackgroundColor = Color.WhiteSmoke,
                        Margin = new Thickness(10,0,10,20),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = 200,
                        WidthRequest = 200,
                        
                    };
                 

                    var Orglabel = new Label
                    {
                        Text = product.Org_CategoryId.ToString(),
                        IsVisible = false
                    };
                    image.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text,product.CategoryImage.ToString())),
                    });
                    shop.Children.Add(image, columnIndex, rowIndex);
                    shop.Children.Add(label, columnIndex, rowIndex);
                    shop.Children.Add(Orglabel, columnIndex, rowIndex);
                   
                }
            }
            //shop.ItemsSource = DataService.Instance.ShopDetails;
        }

        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;

        }
        
        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;

        }
        private  void TapGestureRecognizer_Tapped(string orgId,string Img)
        {
            //var neworgId = Convert.ToInt32(orgId);
            //if(neworgId != oldorgId)
            //{
            //    Xamarin.Essentials.SecureStorage.RemoveAll();
            //}

            SecureStorage.SetAsync("OrgCatId",orgId);
            //SecureStorage.SetAsync("ImgId", Img);

            var org = Convert.ToInt32(orgId);
            Navigation.PushAsync(new OrgPage(org));
            
        }

        void OrderInfoClick(System.Object sender, System.EventArgs e)
        {
        }

    }
   
}