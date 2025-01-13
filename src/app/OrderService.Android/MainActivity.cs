using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using OrderService.Droid.Services;
using OrderService.Interfaces;
using OrderService.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

namespace OrderService.Droid
{
    [Activity(Label = "Emissor Digital de O.S.", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //setConfigKeyboard
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            //setOnlyLightTheme
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

            //initCurrentActivity
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //initCrossCurrentActivity
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //initUserDialogs
            UserDialogs.Init(this);

            //initFirebaseApp
            Firebase.FirebaseApp.InitializeApp(this);

            //initInputKit
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);

            //registerServices
            DependencyService.Register<IFirebaseRemoteConfigService, FirebaseRemoteConfigService>();
            DependencyService.Register<IFirebaseAuthService, FirebaseAuthService>();
            DependencyService.Register<IGoogleManager, GoogleManager>();
            DependencyService.Register<IClientService, ClientService>();
            DependencyService.Register<IItemService, ItemService>();
            DependencyService.Register<IOrderService, OrderService.Services.OrderService>();
            DependencyService.Register<IUserService, UserService>();
            DependencyService.Register<IPropertiesService, PropertiesService>();
            DependencyService.Register<IDialogService, DialogService>();

            //startupComponents
            DependencyService.Get<IFirebaseRemoteConfigService>().Init();

            LoadApplication(new App());

            //requestPermission
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Tiramisu)
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.PostNotifications) != (int)Permission.Granted)
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.PostNotifications }, 1);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(intent);
                GoogleManager.Instance.OnAuthCompleted(result);
            }
        }
    }
}