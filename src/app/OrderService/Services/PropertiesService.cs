using Newtonsoft.Json;
using OrderService.AppConstant;
using OrderService.Interfaces;
using Xamarin.Forms;

namespace OrderService.Services
{
    public class PropertiesService : IPropertiesService
    {
        public bool IsToResetPushNotificationToken()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey(Constants.ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION))
                {
                    var resetedVersion = JsonConvert.DeserializeObject<string>(Application.Current.Properties[Constants.ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION] as string);
                    var currentVersion = Plugin.DeviceInfo.CrossDeviceInfo.Current.AppBuild;

                    if (resetedVersion != null && resetedVersion.ToString() != currentVersion)
                        return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return false;
        }

        public async Task SetLastResetTokenVersion()
        {
            var version = Plugin.DeviceInfo.CrossDeviceInfo.Current.AppBuild;

            if (Application.Current.Properties.ContainsKey(Constants.ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION))
            {
                Application.Current.Properties[Constants.ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION] = version;
            }
            else
            {
                Application.Current.Properties.Add(Constants.ANDROID_PUSHNOTIFICATIONTOKEN_RESET_VERSION, version);
            }

            await Application.Current.SavePropertiesAsync();
        }
    }
}