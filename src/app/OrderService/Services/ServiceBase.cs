using Acr.UserDialogs;
using OrderService.Api.Client;
using OrderService.AppConstant;
using OrderService.Configuration;
using OrderService.Extensions;
using OrderService.Interfaces;
using OrderService.Models.Firebase;
using Xamarin.Forms;

namespace OrderService.Services
{
    public class ServiceBase
    {
        protected OrderServiceApiClient _client;

        public ServiceBase()
        {
            var urls = DependencyService.Get<IFirebaseRemoteConfigService>().GetAsync<Urls>(Constants.FIREBASE_KEY_URLS).Result;
            string? baseUrl = DependencyService.Get<IConfigurationSettings>().Environment switch
            {
                ConfigurationSettings.DeploymentEnvironmentEnum.Development => Constants.DEBUG_API_IN_PRODUCTION ? urls.BaseUrlProduction : Constants.DEBUG_API_URL,
                ConfigurationSettings.DeploymentEnvironmentEnum.Production => urls.BaseUrlProduction,
                _ => throw new Exception("Fail to GetBaseUrl"),
            };
            _client = new OrderServiceApiClient(baseUrl, new HttpClient());
        }

        protected void OnError(Exception ex)
        {
            var error = ex.InnerMost();

            if (ex.GetType() == typeof(ApiException<ProblemDetails>))
                error = ((ApiException<ProblemDetails>)ex).Result.Title;

            if (ex.GetType() == typeof(ApiException<Error>))
                error = ((ApiException<Error>)ex).Result.Description;

            if (ex.GetType() == typeof(ApiException<System.Collections.Generic.ICollection<Error>>))
                error = ((ApiException<System.Collections.Generic.ICollection<Error>>)ex).Result.First().Description;

            if (ex.GetType() == typeof(ApiException<System.Collections.Generic.IEnumerable<Error>>))
                error = ((ApiException<System.Collections.Generic.IEnumerable<Error>>)ex).Result.First().Description;

            if (error.ToLower().Contains("unauthorized"))
            {
                DependencyService.Get<IDialogService>().ShowErrorToast("Sessão expirada. Faça login novamente");
                Application.Current.MainPage = new MainPage(true, true);
            }
            else
            {
                if (error.Length <= 50)
                    DependencyService.Get<IDialogService>().ShowErrorToast(error);
                else
                    UserDialogs.Instance.Alert(error, "Alerta!", "Ok");
            }
        }
    }
}