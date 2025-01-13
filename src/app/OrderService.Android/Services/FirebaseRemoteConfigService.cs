using OrderService.Interfaces;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using Android.Gms.Extensions;
using OrderService.AppConstant;

namespace OrderService.Droid.Services
{
    public class FirebaseRemoteConfigService : IFirebaseRemoteConfigService
    {
        public async Task Init()
        {
            await FirebaseRemoteConfig.Instance.SetDefaultsAsync(Resource.Xml.RemoteConfigDefaults);
            await FirebaseRemoteConfig.Instance.Fetch(Constants.SECONDS_CACHE_FIREBASE_REMOTE_CONFIG);
            await FirebaseRemoteConfig.Instance.Activate();
        }

        public async Task<TInput> GetAsync<TInput>(string key)
        {
            var settings = FirebaseRemoteConfig.Instance.GetString(key);
            return await Task.FromResult(Newtonsoft.Json.JsonConvert.DeserializeObject<TInput>(settings));
        }
    }
}