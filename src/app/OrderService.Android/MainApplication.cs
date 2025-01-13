using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using OrderService.Interfaces;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Droid
{
#if DEBUG
    [Application(Debuggable = true, UsesCleartextTraffic = true)]
#else

    [Application(Debuggable = false, UsesCleartextTraffic = true)]
#endif
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            try
            {
                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";
                    FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
                }
#if DEBUG
                FirebasePushNotificationManager.Initialize(this, true);
#else
                FirebasePushNotificationManager.Initialize(this, false);
#endif
                Task.Run(() => ChecIfIsToResetPushNotificationToken());

                //Handle notification when app is closed here
                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) => SendNotification(p.Data);
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) => Task.Run(() => SendPushNotificationToken(p.Token));
                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) => { };
                CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) => { };
                CrossFirebasePushNotification.Current.RegisterForPushNotifications();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro registrando para PushNotifications: " + ex.Message);
            }
        }

        private void ChecIfIsToResetPushNotificationToken()
        {
            try
            {
                Thread.Sleep(1000);
                IPropertiesService propertiesService = null;
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        propertiesService = Xamarin.Forms.DependencyService.Get<IPropertiesService>();
                        break;
                    }
                    catch { }
                }

                if (propertiesService == null)
                    return;

                bool isToResetPushNotificationToken = propertiesService.IsToResetPushNotificationToken();
                if (isToResetPushNotificationToken)
                {
                    propertiesService.SetLastResetTokenVersion();
                    FirebasePushNotificationManager.Initialize(this, true);
                }
            }
            catch { }
        }

        private async Task SendPushNotificationToken(string newToken)
        {
            try
            {
                Thread.Sleep(1000);
                IUserService userService = null;
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        userService = Xamarin.Forms.DependencyService.Get<IUserService>();
                        break;
                    }
                    catch { }
                }

                if (userService == null)
                    return;

                await userService.AddPushNotificationTokenAsync(newToken);
            }
            catch { }
        }

        private void SendNotification(IDictionary<string, object> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);

            foreach (var key in data.Keys)
                intent.PutExtra(key, data[key].ToString());

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.Mutable);

            var notificationBuilder = new NotificationCompat.Builder(this, FirebasePushNotificationManager.DefaultNotificationChannelId)
                                      .SetSmallIcon(Resource.Mipmap.ic_launcher)
                                      .SetContentTitle(data["title"].ToString())
                                      .SetContentText(data["body"].ToString())
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}