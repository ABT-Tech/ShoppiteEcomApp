using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Models;
using DellyShopApp.Services;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using DellyShopApp.Views.TabbedPages;
using Plugin.FirebasePushNotification;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DellyShopApp
{
    public partial class App
    {
        public int VendorUserId = Convert.ToInt32(SecureStorage.GetAsync("VendorUserId").Result);
        public int UserId = Convert.ToInt32(SecureStorage.GetAsync("UserId").Result);
        public App(bool hasNotification = false, IDictionary<string, object> notificationData = null)
        {
            InitializeComponent();
            FlowListView.Init();
            Device.SetFlags(new[] {
               "SwipeView_Experimental",
               "DragAndDrop_Experimental",
               "Shapes_Experimental"
            });
            if (Settings.SelectLanguage == "")
            {
               Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
               AppResources.Culture = new CultureInfo("en");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.SelectLanguage);
                AppResources.Culture = new CultureInfo(Settings.SelectLanguage);
            }
            CrossFirebasePushNotification.Current.Subscribe("general");
            //Token event usage sample:
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                FirebaseToken firebaseToken = new FirebaseToken();
                firebaseToken.MacID = GetDeviceInfo();
                firebaseToken.Token = p.Token;
                firebaseToken.UserID = UserId;
                SetFirebaseToken(firebaseToken);
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                Xamarin.Essentials.SecureStorage.SetAsync("FirebaseToken",p.Token);
                Xamarin.Essentials.SecureStorage.SetAsync("MacId", firebaseToken.MacID);
            };

            //Push message received event usage sample:
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };
            //Push message opened event usage sample:

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };
            //Push message action tapped event usage sample: OnNotificationAction
            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }
                }
            };
            //Push message deleted event usage sample: (Android Only)
            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Deleted");
            };
            if (!hasNotification)
            {
                MainPage navigation = new MainPage();
                MainPage = new NavigationPage(new MainPage());
                NavigationPage navpage = new NavigationPage(navigation);
                NavigationPage.SetHasNavigationBar(navpage, false);
                NavigationPage.SetHasNavigationBar(navigation, false);
                MainPage = navpage;

            }
            else
            {
                foreach (var data in notificationData)
                {
                    if (data.Key == "OrgId")
                    {
                        SecureStorage.SetAsync("OrgId", data.Value.ToString());
                    }
                    if (data.Key == "Logo")
                    {
                        SecureStorage.SetAsync("Logo", data.Value.ToString());
                    }
                }

                HomeTabbedPage navigation = new HomeTabbedPage();
                MainPage = new NavigationPage(new HomeTabbedPage());
                NavigationPage navpage = new NavigationPage(navigation);
                NavigationPage.SetHasNavigationBar(navpage, false);
                NavigationPage.SetHasNavigationBar(navigation, false);
                MainPage = navpage;

            }            
            App.Current.MainPage.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }
        //public App(IYmChat iymchat)
        //{
        //    InitializeComponent();
        //    MainPage = new MainPage(iymchat);
        //}
        private string GetDeviceInfo()
        {
            return Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
        public async void SetFirebaseToken(FirebaseToken firebaseToken) 
        {
            await DataService.UpdateFireBaseToken(firebaseToken);
        }

    }

}
