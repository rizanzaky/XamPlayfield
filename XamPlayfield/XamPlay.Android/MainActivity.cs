using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Extensions;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase.Iid;
using Debug = System.Diagnostics.Debug;

namespace XamPlay.Droid
{
    [Activity(Label = "XamPlay", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string ChannelId = "00001111";
        private const string ChannelName = "XamPlay_Droid_Channel";
        
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            
            CreateNotificationChannelIfPlayServicesAvailable();
            _ = GetFcmToken();
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        private static async Task GetFcmToken()
        {
            var instanceIdResult = await FirebaseInstanceId.Instance.GetInstanceId().AsAsync<IInstanceIdResult>();
            Debug.WriteLine(instanceIdResult);
        }
        
        private static void CreateNotificationChannelIfPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Application.Context);
            if (resultCode != ConnectionResult.Success)
            {
                return;
            }

            CreateNotificationChannel();
        }

        private static void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channel = new NotificationChannel(ChannelId, ChannelName, NotificationImportance.Default);

            // Register the channel with the system; you can't change the importance
            // or other notification behaviors after this
            var notificationManager = (NotificationManager)Application.Context.GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}