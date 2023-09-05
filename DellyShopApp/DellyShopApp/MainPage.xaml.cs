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
using System.Collections.Generic;

namespace DellyShopApp
{

    public partial class MainPage
    {
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public MainPage()
        {

            InitializeComponent();
            MyMenu = GetMenus();
            this.BindingContext = this;
            if (ChechConnectivity())
            {
                InittMainPage();
            }

        }
        public List<Menu> MyMenu { get; set; }
        private List<Menu> GetMenus()
        {
            return new List<Menu>
            {
                new Menu{Name = "Home",Icon = "menu.png"},
                new Menu{Name = "Home",Icon = "menu.png"},
                new Menu{Name = "Home",Icon = "menu.png"}
            };
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
            //CarouselViewa.ItemsSource = DataService.Instance.Carousel;
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
                    //var label = new Label
                    //{
                    //    Text = product.CategoryName,
                    //    VerticalOptions = LayoutOptions.End,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    TextColor = Color.Chocolate,
                    //    MaxLines = 1,
                    //    LineBreakMode = LineBreakMode.TailTruncation,
                    //    HorizontalTextAlignment = TextAlignment.Center,
                    //    FontAttributes = FontAttributes.Bold,
                    //    FontSize = 18,


                    //};
                    var image = new Image
                    {

                        Aspect = Aspect.Fill,
                        Source = product.CategoryImage,
                        BackgroundColor = Color.WhiteSmoke,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 80,
                        WidthRequest = 160,
                        Margin = 5


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
                    //shop.Children.Add(label, columnIndex, rowIndex);
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
        private void TapGestureRecognizer_Tapped(string orgId, string Img)
        {
            //var neworgId = Convert.ToInt32(orgId);
            //if(neworgId != oldorgId)
            //{
            //    Xamarin.Essentials.SecureStorage.RemoveAll();
            //}

            SecureStorage.SetAsync("OrgCatId", orgId);
            //SecureStorage.SetAsync("ImgId", Img);

            var org = Convert.ToInt32(orgId);
            Navigation.PushAsync(new OrgPage(org));

        }

        private async void OpenAnimation()
        {
            await swipecontent.ScaleTo(0.9, 300, Easing.SinOut);
            pancake.CornerRadius = 20;
            await swipecontent.RotateTo(-15, 300, Easing.SinOut);
        }
        private async void CloseAnimation()
        {
            await swipecontent.RotateTo(0, 300, Easing.SinOut);
            pancake.CornerRadius = 0;
            await swipecontent.ScaleTo(1, 300, Easing.SinOut);
        }

        void OpenSwipe(System.Object sender, System.EventArgs e)
        {
            mainswipeview.Open(OpenSwipeItem.LeftItems);
            OpenAnimation();
        }
        void CloseSwipe(System.Object sender, System.EventArgs e)
        {
            mainswipeview.Close();
            CloseAnimation();
        }
        void OrderInfoClick(System.Object sender, System.EventArgs e)
        {
        }





        private void SwipeStarted(System.Object sender, Xamarin.Forms.SwipeStartedEventArgs e)
        {
            OpenAnimation();
        }

        private void SwipeEnded(System.Object sender, Xamarin.Forms.SwipeEndedEventArgs e)
        {
            if (!e.IsOpen)
                CloseAnimation();
        }
        public class Menu
        {
            public string Name { get; set; }
            public string Icon { get; set; }
        }

    }

}