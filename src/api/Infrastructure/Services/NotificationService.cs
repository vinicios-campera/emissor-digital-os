using AutoMapper;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using System.Linq.Expressions;
using Kernel.Net.Http.Interfaces;
using Kernel.Toolkit.Extensions;

namespace OrderService.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly Expression<Func<Notification, bool>> _defaultExpression;

        public NotificationService(IMapper mapper, IUserAccessor userAccessor, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
            _defaultExpression = x => x.UserEmail == userAccessor.GetUserEmail();
        }

        public bool AddNotification(string title, string body, Guid userId, string userEmail)
        {
            var notification = new Notification
            {
                Title = title,
                Body = body,
                UserId = userId,
                UserEmail = userEmail,
                State = NotificationState.New
            };
            notification.SetInsertedDate();
            return _notificationRepository.Insert(notification);
        }

        public NotificationResponse? GetNotificationById(Guid id)
            => _mapper.Map<NotificationResponse>(_notificationRepository.FindById(id).ValidateIsNull());

        public IEnumerable<NotificationResponse>? GetNotifications(Expression<Func<Notification, bool>> expression, bool setNewsAsRead)
        {
            var mapped = _mapper.Map<IEnumerable<NotificationResponse>>(_notificationRepository.Find(_defaultExpression.And(expression)));
            if (setNewsAsRead)
                foreach (var item in mapped.Where(x => x.State == NotificationState.New))
                    SetState(item.Id, NotificationState.Read);
            return mapped.Where(x => x.State != NotificationState.Error);
        }

        public bool SetState(Guid id, NotificationState state, string? error = null)
        {
            var notification = _notificationRepository.FindById(id).ValidateIsNull();
            notification!.State = state;
            notification!.Error = error;
            notification!.SetModifiedDate();
            return _notificationRepository.Update(notification);
        }
    }
}