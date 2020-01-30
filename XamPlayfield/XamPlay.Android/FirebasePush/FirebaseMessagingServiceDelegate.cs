using System.Diagnostics;
using Android.App;
using Firebase.Messaging;

namespace XamPlay.Droid.FirebasePush
{
    [Service(Name = "com.devariz.xamplay.FirebaseMessagingServiceDelegate", Exported = false)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingServiceDelegate : FirebaseMessagingService
    {
        private static int NotificationId { get; set; } = 100;

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
            
            Debug.WriteLine(p0);
        }

        public override async void OnMessageReceived(RemoteMessage p0)
        {
            var notification = p0.GetNotification();
            
            base.OnMessageReceived(p0);
        }
    }
}