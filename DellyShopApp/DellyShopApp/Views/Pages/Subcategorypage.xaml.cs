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
    
    public partial class Subcategorypage
    {
        public OrgCategories orgCategories;
        public int oldorgId = Convert.ToInt32(SecureStorage.GetAsync("OrgId").Result);
        public int MainCategoryId = Convert.ToInt32(SecureStorage.GetAsync("Org_CategoryId").Result);

        public Subcategorypage(Category category)
        {
           
            
            InitializeComponent();
            if (ChechConnectivity())
            {
                InittSubcategorypage(category);
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
        private async void InittSubcategorypage(Category category)
        {
            Busy();
            this.BindingContext = this;
            List<Category> categories = new List<Category>();
            var AllOrganizations = await DataService.GetAllSubCategories(category.MainCategoryId); //DataService.Instance.ShopDetails;
           StackLabelCategoryName.Text =category.MainCategory ;
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
                for (int columnIndex = 0; columnIndex < 3; columnIndex++)
                {
                    if (productIndex >= AllOrganizations.Count)
                    {
                        break;
                    }
                    var product = AllOrganizations[productIndex];
                    productIndex += 1;
                    //var label = new Label
                    //{
                    //    Text = product.ShopName,
                    //    VerticalOptions = LayoutOptions.End,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    TextColor = Color.Chocolate,
                    //    FontAttributes = FontAttributes.Bold,
                    //    MaxLines = 1,
                    //    LineBreakMode = LineBreakMode.TailTruncation,
                    //    HorizontalTextAlignment = TextAlignment.Center,
                    //    FontSize = 18,
                        

                    //};
                    //if (product.IsPublished == false )
                    //{
                    //    var image = new Image
                    //    {

                    //        Aspect = Aspect.AspectFit,
                    //        Source = product.Image,
                    //        Opacity = 0.5,
                    //        BackgroundColor = Color.WhiteSmoke,
                    //        Margin = new Thickness(0, 15, 0, 25),
                    //        VerticalOptions = LayoutOptions.Center,
                    //        HorizontalOptions = LayoutOptions.Center,
                    //        HeightRequest = 200,
                    //        WidthRequest = 200,

                    //    };
                    //    var Orglabel = new Label
                    //    {
                    //        Text = product.OrgId.ToString(),
                    //        IsVisible = false
                    //    };
                    //    var image1 = new Image
                    //    {

                    //        Aspect = Aspect.AspectFit,
                    //        Source = "soon.png",
                    //        Margin = new Thickness(0, 15, 0, 25),
                    //        VerticalOptions = LayoutOptions.Start,
                    //        HorizontalOptions = LayoutOptions.FillAndExpand,
                    //        HeightRequest = 200,
                    //        WidthRequest = 200,
                    //    };
                    //    shop.Children.Add(image, columnIndex, rowIndex);
                    //    shop.Children.Add(image1, columnIndex, rowIndex);
                    //    shop.Children.Add(Orglabel, columnIndex, rowIndex);
                    //}
                    //else
                    //{
                        var image = new Image
                        {

                            Aspect = Aspect.AspectFit,
                            Source = product.CategoryImage,
                            BackgroundColor = Color.WhiteSmoke,
                            Margin = new Thickness(0, 15, 0, 25),
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 100,
                            WidthRequest = 100,

                        };
                       
                        shop.Children.Add(image, columnIndex, rowIndex);

                    //image.GestureRecognizers.Add(new TapGestureRecognizer
                    //{
                    //    Command = new Command(() => TapGestureRecognizer_Tapped(product.MainCategoryImage)),
                    //});
                    //}








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
        //private  void TapGestureRecognizer_Tapped(string orgId,string Img)
        //{
        //    var neworgId = Convert.ToInt32(orgId);
        //    if (neworgId != oldorgId)
        //    {
        //        Xamarin.Essentials.SecureStorage.RemoveAll();
        //    }

        //    SecureStorage.SetAsync("OrgId",orgId);
        //    SecureStorage.SetAsync("ImgId", Img);

        //    Navigation.PushAsync(new HomeTabbedPage());
            
        //}

       private async void BackPage(System.Object sender, System.EventArgs e)
        {
           await Navigation.PopAsync();
        }

        void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        {
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
   
}