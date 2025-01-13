using OrderService.AppConstant;
using OrderService.Interfaces;
using OrderService.Models.Firebase;
using OrderService.Pages.MainMenu;
using Plugin.FirebasePushNotification;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderService
{
    public partial class MainPage : ContentPage
    {
        private readonly bool _isLogoffing;

        public MainPage(bool isLogoffing = false, bool isExpired = false)
        {
            _isLogoffing = isLogoffing;

            InitializeComponent();

            if (isExpired)
                DependencyService.Get<IDialogService>().ShowErrorToast("Sessão expirada. Faça login novamente");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_isLogoffing)
            {
                try
                {
                    SecureStorage.RemoveAll();
                    DependencyService.Get<IGoogleManager>().Logout();
                }
                catch { }
            }

            VerifyExpirationTokenFromStorage();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var token = string.Empty;
            try
            {
                token = await DependencyService.Get<IFirebaseAuthService>().LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            }
            catch { }
            setupInitial(token);
        }

        private void Login_Google_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IGoogleManager>().Login(async (string idToken, string message) =>
            {
                var token = string.Empty;
                try
                {
                    token = await DependencyService.Get<IFirebaseAuthService>().LoginWithGoogle(idToken);
                }
                catch { }
                setupInitial(token);
            });
        }

        private async void setupInitial(string token)
        {
            if (token != "")
            {
                await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_TOKEN, token);

                var userData = await DependencyService.Get<IUserService>().GetUserAsync();

                if (userData != null)
                {
                    await SecureStorage.SetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED, userData.PrivacyPolicyAccepted.ToString());
                    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_ADD_PICTURE_ORDER, userData.AddPictureInOrder.ToString());
                    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_EMAIL, userData.Email);
                    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_PICTURE, userData.PictureUrl);

                    if (!userData.PrivacyPolicyAccepted)
                    {
                        var actionSheet = await App.Current.MainPage.DisplayActionSheet("Você precisa concordar com os termos deste app", "Cancelar", null, "Concordo", "Não concordo", "Abrir Politica de Privacidade");
                        switch (actionSheet)
                        {
                            case "Concordo":
                                userData.PrivacyPolicyAccepted = await DependencyService.Get<IUserService>().AcceptPolicyAsync();
                                await SecureStorage.SetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED, userData.PrivacyPolicyAccepted.ToString());
                                break;

                            case "Abrir Politica de Privacidade":
                                var configUrl = await DependencyService.Get<IFirebaseRemoteConfigService>().GetAsync<Urls>(Constants.FIREBASE_KEY_URLS);
                                await Browser.OpenAsync(new Uri(configUrl.PrivacyPolicy), BrowserLaunchMode.SystemPreferred);
                                break;
                        }
                    }

                    if (userData.PrivacyPolicyAccepted)
                    {
                        if (userData.PushNotificationToken != CrossFirebasePushNotification.Current.Token)
                            await DependencyService.Get<IUserService>().AddPushNotificationTokenAsync(CrossFirebasePushNotification.Current.Token);

                        App.Current.MainPage = new NavigationPage(new MainMenuPage());
                    }
                }
            }
            else
            {
                await DisplayAlert("Falha ao realizar login", "E-mail ou senha estão incorreto. Tente novamente!", "OK");
            }

            #region OLD

            //var configOidc = await DependencyService.Get<IRemoteConfigurationService>().GetAsync<OpenId>(Constants.FIREBASE_KEY_OPENId);

            //var browser = DependencyService.Get<IBrowser>();

            //var options = new OidcClientOptions
            //{
            //    Authority = configOidc.Authority,
            //    ClientId = configOidc.ClientId,
            //    ClientSecret = configOidc.ClientSecret,
            //    Scope = configOidc.Scope,
            //    RedirectUri = configOidc.RedirectUrl,
            //    Browser = browser
            //};

            //var _client = new OidcClient(options);
            //var _result = await _client.LoginAsync(new LoginRequest());

            //if (!_result.IsError)
            //{
            //    var token = _result.AccessToken;

            //    var jwtToken = new JwtSecurityToken(token);

            //    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_TOKEN, token);
            //    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_EMAIL, jwtToken.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value);
            //    await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_PICTURE, jwtToken.Claims.FirstOrDefault(x => x.Type == "picture").Value);

            //    var userData = await DependencyService.Get<IUserService>().GetUserAsync();

            //    if (userData != null)
            //    {
            //        await SecureStorage.SetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED, userData.PrivacyPolicyAccepted.ToString());
            //        await SecureStorage.SetAsync(Constants.KEY_STORAGE_USER_ADD_PICTURE_ORDER, userData.AddPictureInOrder.ToString());

            //        if (!userData.PrivacyPolicyAccepted)
            //        {
            //            var actionSheet = await App.Current.MainPage.DisplayActionSheet("Você precisa concordar com os termos deste app", "Cancelar", null, "Concordo", "Não concordo", "Abrir Politica de Privacidade");
            //            switch (actionSheet)
            //            {
            //                case "Concordo":
            //                    userData.PrivacyPolicyAccepted = await DependencyService.Get<IUserService>().AcceptPolicyAsync();
            //                    await SecureStorage.SetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED, userData.PrivacyPolicyAccepted.ToString());
            //                    break;

            //                case "Abrir Politica de Privacidade":
            //                    var configUrl = await DependencyService.Get<IRemoteConfigurationService>().GetAsync<Urls>(Constants.FIREBASE_KEY_URLS);
            //                    await Browser.OpenAsync(new Uri(configUrl.PrivacyPolicy), BrowserLaunchMode.SystemPreferred);
            //                    break;
            //            }
            //        }

            //        if (userData.PrivacyPolicyAccepted)
            //        {
            //            if (userData.PushNotificationToken != CrossFirebasePushNotification.Current.Token)
            //                await DependencyService.Get<IUserService>().AddPushNotificationTokenAsync(CrossFirebasePushNotification.Current.Token);

            //            App.Current.MainPage = new NavigationPage(new MainMenuPage());
            //        }
            //    }
            //}

            #endregion OLD
        }

        private void VerifyExpirationTokenFromStorage()
        {
            var token = SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN).Result;

            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = new JwtSecurityToken(token);
                if (jwtToken != null && (jwtToken.ValidTo > DateTime.UtcNow))
                {
                    var acepted = SecureStorage.GetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED).Result;
                    if (!string.IsNullOrEmpty(acepted) && Convert.ToBoolean(acepted))
                        App.Current.MainPage = new NavigationPage(new MainMenuPage());
                }
            }
        }
    }
}