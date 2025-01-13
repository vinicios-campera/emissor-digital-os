using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using System.Linq.Expressions;

namespace OrderService.Application.Interfaces.Services
{
    public interface INotificationService
    {
        bool AddNotification(string title, string body, Guid userId, string userEmail);

        IEnumerable<NotificationResponse>? GetNotifications(Expression<Func<Notification, bool>> expression, bool setNewsAsRead);

        NotificationResponse? GetNotificationById(Guid id);

        bool SetState(Guid id, NotificationState state, string? error = null);
    }
}