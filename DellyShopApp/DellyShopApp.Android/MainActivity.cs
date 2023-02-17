using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using DellyShopApp.Droid.Renderers;
using FFImageLoading.Forms.Platform;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Plugin.FirebasePushNotification;
using Plugin.PayCards;
using Xamarin.Forms;
namespace DellyShopApp.Droid
{
    [Activity(Label = "DellyShopApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            initFontScale();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            base.OnCreate(savedInstanceState);

           
            ///For Performance 
            Forms.SetFlags("FastRenderers_Experimental");

            AndroidEnvironment.UnhandledExceptionRaiser -= StoreLogger;
            AndroidEnvironment.UnhandledExceptionRaiser += StoreLogger;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var config = new PayPalConfiguration(PayPalEnvironment.NoNetwork, "Your PayPal ID from https://developer.paypal.com/developer/applications/")
            {
                //If you want to accept credit cards
                AcceptCreditCards = true,
                //Your business name
                MerchantName = "Test Store",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://www.example.com/privacy",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://www.example.com/legal",
                // OPTIONAL - ShippingAddressOption (Both, None, PayPal, Provided)
                ShippingAddressOption = ShippingAddressOption.Both,
                // OPTIONAL - Language: Default languege for PayPal Plug-In
                Language = "es",
                // OPTIONAL - PhoneCountryCode: Default phone country code for PayPal Plug-In
                PhoneCountryCode = "52",
            };
            CrossPayPalManager.Init(config, this);
            CardsViewRenderer.Preserve();
            CachedImageRenderer.InitImageViewHandler();
           // PayCardsRecognizerService.Initialize(this);
            LoadApplication(new App());
            //Activate after adding the Google-Service.json file.
            //https://github.com/CrossGeeks/FirebasePushNotificationPlugin/blob/master/docs/GettingStarted.md
            //FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
        protected override void OnNewIntent(Intent intent)
        {
            //Activate after adding the Google-Service.json file.
            //https://github.com/CrossGeeks/FirebasePushNotificationPlugin/blob/master/docs/GettingStarted.md
            base.OnNewIntent(intent);
            //FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
        /// <summary>
        /// Global try catch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoreLogger(object sender, RaiseThrowableEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        /// <summary>
        /// All app Font size 1
        /// </summary>
        private void initFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1;
            //0.85 small, 1 standard, 1.15 big，1.3 more bigger ，1.45 supper big
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.Resources.UpdateConfiguration(configuration, metrics);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
           // PayCardsRecognizerService.OnActivityResult(requestCode, resultCode, data);

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }
    }
}