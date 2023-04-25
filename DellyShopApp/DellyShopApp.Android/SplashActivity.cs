using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Plugin.FirebasePushNotification;

namespace DellyShopApp.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            StartActivity(typeof(MainActivity));
            FirebasePushNotificationManager.ProcessIntent(this,Intent);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }
        protected override void OnNewIntent(Intent intent)
        {
            FirebasePushNotificationManager.ProcessIntent(this,intent);
            base.OnNewIntent(intent);
        }
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            RunOnUiThread(() => {
                Task startupWork = new Task(() => { SimulateStartup(); });
                startupWork.Start();
            });
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
    }
}