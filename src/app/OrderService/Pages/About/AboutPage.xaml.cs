using OrderService.AppConstant;
using OrderService.Configuration;
using OrderService.Interfaces;
using OrderService.Models.Firebase;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.About
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public static readonly BindableProperty VersionProperty = BindableProperty.Create(nameof(Version), typeof(string), typeof(AboutPage), default(string));

        public static readonly BindableProperty EnvironmentProperty = BindableProperty.Create(nameof(Environment), typeof(string), typeof(AboutPage), default(string));

        public static readonly BindableProperty AddPhotoProperty = BindableProperty.Create(nameof(AddPhoto), typeof(bool), typeof(AboutPage), false);

        public AboutPage()
        {
            InitializeComponent();

            SetValue(VersionProperty, $"{VersionTracking.CurrentVersion} Build {VersionTracking.CurrentBuild}");
            SetValue(EnvironmentProperty, $"{DependencyService.Get<IConfigurationSettings>().Environment}");

            var addPhoto = SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_ADD_PICTURE_ORDER).Result;

            if (!string.IsNullOrEmpty(addPhoto))
                SetValue(AddPhotoProperty, Convert.ToBoolean(addPhoto));
        }

        public string Version
        {
            get => (string)GetValue(VersionProperty);
        }

        public string Environment
        {
            get => (string)GetValue(EnvironmentProperty);
        }

        public bool AddPhoto
        {
            get => (bool)GetValue(AddPhotoProperty);
        }

        private async void PrivacyPolicy(object sender, EventArgs e)
        {
            var config = await DependencyService.Get<IFirebaseRemoteConfigService>().GetAsync<Urls>(Constants.FIREBASE_KEY_URLS);

            var acepted = await SecureStorage.GetAsync(Constants.KEY_STORAGE_PRIVACY_POLICY_ACCEPTED);

            if (!Convert.ToBoolean(acepted))
            {
                var actionSheet = await App.Current.MainPage.DisplayActionSheet("Você precisa concordar com os termos deste app", "Cancelar", null, "Concordo", "Não concordo", "Abrir Politica de Privacidade");
                switch (actionSheet)
                {
                    case "Concordo":
                        await DependencyService.Get<IUserService>().AcceptPolicyAsync();
                        break;

                    case "Abrir Politica de Privacidade":
                        await Browser.OpenAsync(new Uri(config.PrivacyPolicy), BrowserLaunchMode.SystemPreferred);
                        break;
                }
            }
            else
            {
                await Browser.OpenAsync(new Uri(config.PrivacyPolicy), BrowserLaunchMode.SystemPreferred);
            }
        }

        private async void DeleteAccount(object sender, EventArgs e)
        {
            var deleteAccount = await App.Current.MainPage.DisplayAlert("Que pena", "Deseja realmente excluir sua conta? TODOS os seus dados do aplicativo serão excluídos, tais como Clientes, Produtos, Ordens de Serviço, Email, Dados pessoais e etc...", "Sim", "Não");
            if (deleteAccount)
            {
                var accountDeleted = await DependencyService.Get<IUserService>().DeleteAccountAsync();
                if (accountDeleted)
                {
                    DependencyService.Get<IDialogService>().ShowSucessToast("Exclusão bem sussedida. Sua conta e seus dados foram todos removidos");
                    App.Current.MainPage = new NavigationPage(new MainPage(true));
                }
            }
        }

        private async void SwitchAddPhoto(object sender, ToggledEventArgs e)
        {
            SetValue(AddPhotoProperty, e.Value);
            await DependencyService.Get<IUserService>().UpdateAddPhotoAsync(e.Value);
        }

        private async void PersonalData(object sender, EventArgs e)
        {
            var config = await DependencyService.Get<IFirebaseRemoteConfigService>().GetAsync<OpenId>(Constants.FIREBASE_KEY_OPENId);

            await App.Current.MainPage.DisplayAlert("Atenção",
                "O cadastro solicitado no formulário a seguir, é totalmente OPCIONAL. O não preenchimento dele, é indiferente para o uso do aplicativo. " +
                "Os dados são usadas somente para adição no rodapé do PDF quando criado uma nova O.S. Eles poderão ser alterados ou excluidos quando você bem entender ;)", "Ok");
            await Browser.OpenAsync(new Uri(config.Account), BrowserLaunchMode.SystemPreferred);
        }
    }
}