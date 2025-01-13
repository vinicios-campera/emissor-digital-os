using AutoMapper;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.AutoMapper.Mappers
{
    public class NotificationMapper : Profile
    {
        public NotificationMapper()
        {
            CreateMap<Notification, NotificationResponse>();
        }
    }
}