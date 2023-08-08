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
    
    public partial class OrgPage
    {
        public OrgCategories orgCategories;
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int Org_CategoryId = Convert.ToInt32(SecureStorage.GetAsync("Org_CategoryId").Result);

        public OrgPage(int orgId)
        {
            Org_CategoryId = orgId;
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
            var AllOrganizations = await DataService.GetAllOrganizations(Org_CategoryId); //DataService.Instance.ShopDetails;
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
                        Text = product.ShopName,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Chocolate,
                        FontAttributes = FontAttributes.Bold,
                        MaxLines = 1,
                        LineBreakMode = LineBreakMode.TailTruncation,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        

                    };
                    if (product.IsPublished == false )
                    {
                        var image = new Image
                        {

                            Aspect = Aspect.AspectFit,
                            Source = product.Image,
                            Opacity = 0.5,
                            BackgroundColor = Color.WhiteSmoke,
                            Margin = new Thickness(0, 15, 0, 25),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 200,
                            WidthRequest = 200,

                        };
                        var Orglabel = new Label
                        {
                            Text = product.OrgId.ToString(),
                            IsVisible = false
                        };
                        var image1 = new Image
                        {

                            Aspect = Aspect.AspectFit,
                            Source = "soon.png",
                            Margin = new Thickness(0, 15, 0, 25),
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 200,
                            WidthRequest = 200,
                        };
                        shop.Children.Add(image, columnIndex, rowIndex);
                        shop.Children.Add(image1, columnIndex, rowIndex);
                        shop.Children.Add(Orglabel, columnIndex, rowIndex);
                    }
                    else
                    {
                        var image = new Image
                        {

                            Aspect = Aspect.AspectFit,
                            Source = product.Image,
                            
                            BackgroundColor = Color.WhiteSmoke,
                            Margin = new Thickness(0, 15, 0, 25),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 200,
                            WidthRequest = 200,

                        };
                        var Orglabel = new Label
                        {
                            Text = product.OrgId.ToString(),
                            IsVisible = false
                        };
                        shop.Children.Add(image, columnIndex, rowIndex);
                        shop.Children.Add(Orglabel, columnIndex, rowIndex);
                        image.GestureRecognizers.Add(new TapGestureRecognizer
                        {
                            Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text, product.Image.ToString())),
                        });
                    }
                   

                   
                  
                    
                    shop.Children.Add(label, columnIndex, rowIndex);
                  
                   
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
            var neworgId = Convert.ToInt32(orgId);
            if (neworgId != oldorgId)
            {
                Xamarin.Essentials.SecureStorage.RemoveAll();
            }

            SecureStorage.SetAsync("OrgId",orgId);
            SecureStorage.SetAsync("ImgId", Img);

            Navigation.PushAsync(new HomeTabbedPage());
            
        }

       private async void BackPage(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
   
}