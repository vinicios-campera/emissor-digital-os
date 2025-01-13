using Acr.UserDialogs;
using OrderService.Api.Client;
using OrderService.AppConstant;
using OrderService.Interfaces;
using Xamarin.Essentials;

namespace OrderService.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public async Task<bool> AcceptPolicyAsync()
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Aceitando potica de privacidade", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.AcceptPrivacyPolicyAsync();
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> AddPushNotificationTokenAsync(string pushToken)
        {
            try
            {
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.UpdatePushNotificationTokenAsync(pushToken);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteAccountAsync()
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Excluindo conta", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.DeleteAccountAsync();
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }

        public async Task<IEnumerable<NotificationResponse>?> GetNotificationsAsync(int top, int skíp)
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo noitificações", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetNotificationsAsync(top: top.ToString(), skip: skíp.ToString());
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<UserResponse?> GetUserAsync()
        {
            try
            {
                using var dialog = UserDialogs.Instance.Loading("Obtendo dados", maskType: MaskType.Black);
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.GetUserAsync();
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return null;
            }
        }

        public async Task<bool> UpdateAddPhotoAsync(bool insert)
        {
            try
            {
                var token = await SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_TOKEN);
                _client.SetBearerToken(token);
                var result = await _client.UpdateAddPictureInOrderAsync(insert);
                return result;
            }
            catch (Exception ex)
            {
                OnError(ex);
                return false;
            }
        }
    }
}