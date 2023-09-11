using System.Globalization;
using System.Threading;
using DellyShopApp.Helpers;
using DellyShopApp.Languages;
using DellyShopApp.Views.CustomView;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using DellyShopApp.DbModels;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DellyShopApp
{
    public partial class App
    {
        static SQLiteHelper db;
        public App()
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
            MainPage navigation = new MainPage();
           // MainPage = new NavigationPage(new MainPage());
            NavigationPage navpage = new NavigationPage(navigation);
            NavigationPage.SetHasNavigationBar(navpage, false);
           // NavigationPage.SetHasNavigationBar(navigation, false);
            MainPage = navpage;
            App.Current.MainPage.FlowDirection = Settings.SelectLanguage == "ar" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        }
        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
                }
                return db;
            }
        }
    }
}
