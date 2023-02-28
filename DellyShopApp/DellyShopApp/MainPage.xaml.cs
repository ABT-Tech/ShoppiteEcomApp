using System;
using DellyShopApp.Models;
using DellyShopApp.Languages;
using DellyShopApp.Views.Pages;
using Xamarin.Forms;
using DellyShopApp.Services;
using Xamarin.Essentials;

namespace DellyShopApp
{    
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            float rows = (float)DataService.Instance.ShopDetails.Count / 2;
            double rowcount = Math.Round(rows);
            if (DataService.Instance.ShopDetails.Count % 2 == 1)
            {
                rowcount = rowcount + 1;
            }
            var productIndex = 0;
            for (int rowIndex = 0; rowIndex < rowcount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                {
                    if (productIndex >= DataService.Instance.ShopDetails.Count)
                    {
                        break;
                    }
                    var product = DataService.Instance.ShopDetails[productIndex];
                    productIndex += 1;
                    var label = new Label
                    {
                        Text = product.ShopName,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor= Color.Chocolate,
                        FontAttributes= FontAttributes.Bold,
                        FontSize = 20, 
                        Padding = -5,
                        
                    };
                    var image = new Image
                    {
                        Source= product.Image,
                        HeightRequest= 200,
                        WidthRequest= 200,
                        BackgroundColor = Color.LightBlue,
                        Margin = 15,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                       
                    };
                    var Orglabel = new Label
                    {
                        Text = product.OrgId.ToString(),
                        IsVisible = false
                    };
                    image.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => TapGestureRecognizer_Tapped(Orglabel.Text)),
                    });
                    shop.Children.Add(image, columnIndex, rowIndex);
                    shop.Children.Add(label, columnIndex, rowIndex);
                    shop.Children.Add(Orglabel, columnIndex, rowIndex);
                }
            }
            //shop.ItemsSource = DataService.Instance.ShopDetails;
        }

        private  void TapGestureRecognizer_Tapped(string orgId)
        {
            SecureStorage.SetAsync("OrgId",orgId);
            Navigation.PushAsync(new HomeTabbedPage());
        }      
    }
}