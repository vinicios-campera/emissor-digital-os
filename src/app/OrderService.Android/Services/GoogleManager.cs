using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using OrderService.Interfaces;
using System;
using Xamarin.Forms;

namespace OrderService.Droid.Services
{
    public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public Action<string, string> _onLoginComplete;
        public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }
        private Context _context;

        public GoogleManager()
        {
            _context = global::Android.App.Application.Context;
            Instance = this;
        }

        public void Login(Action<string, string> onLoginComplete)
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("307689100498-figro2ja2ijbpdunia5ncodmkddt5fsr.apps.googleusercontent.com")
                .RequestEmail()
                .Build();

            _googleApiClient = new GoogleApiClient.Builder((_context).ApplicationContext)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .AddScope(new Scope(Scopes.Profile))
                .Build();

            _onLoginComplete = onLoginComplete;
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
            _googleApiClient.Connect();
        }

        public void Logout()
        {
            var gsoBuilder = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestIdToken("307689100498-figro2ja2ijbpdunia5ncodmkddt5fsr.apps.googleusercontent.com")
                .RequestEmail();

            GoogleSignIn.GetClient(_context, gsoBuilder.Build())?.SignOut();

            _googleApiClient.Disconnect();
        }

        public void OnAuthCompleted(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                GoogleSignInAccount account = result.SignInAccount;
                _onLoginComplete?.Invoke(account.IdToken, string.Empty);
            }
            else
            {
                _onLoginComplete?.Invoke(null, "An error occured!");
            }
        }

        public void OnConnected(Bundle connectionHint)
        {
        }

        public void OnConnectionSuspended(int cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }
    }
}