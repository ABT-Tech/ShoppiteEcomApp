using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Views.CustomView;
using DellyShopApp.Views.Pages;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DellyShopApp
{
    public partial class App
    {
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
            //Token event usage sample:
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
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
                    if (data.Key == "LoginPage")
                    {
                        LoginPage loginPage = new LoginPage();
                        MainPage = new NavigationPage(new LoginPage());
                        NavigationPage navpage = new NavigationPage(loginPage);
                        NavigationPage.SetHasNavigationBar(navpage, false);
                        NavigationPage.SetHasNavigationBar(loginPage, false);
                        MainPage = navpage;
                        
                    }
                }
            }
            
            App.Current.MainPage.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        }
    }
}
