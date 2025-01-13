using System.Threading.Tasks;

namespace OrderService.Interfaces
{
    public interface IPropertiesService
    {
        bool IsToResetPushNotificationToken();

        Task SetLastResetTokenVersion();
    }
}