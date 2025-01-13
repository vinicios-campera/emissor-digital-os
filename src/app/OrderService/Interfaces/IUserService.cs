using OrderService.Api.Client;

namespace OrderService.Interfaces
{
    public interface IUserService
    {
        Task<bool> AcceptPolicyAsync();

        Task<bool> UpdateAddPhotoAsync(bool insert);

        Task<bool> AddPushNotificationTokenAsync(string pushToken);

        Task<bool> DeleteAccountAsync();

        Task<UserResponse?> GetUserAsync();

        Task<IEnumerable<NotificationResponse>?> GetNotificationsAsync(int top, int skíp);
    }
}