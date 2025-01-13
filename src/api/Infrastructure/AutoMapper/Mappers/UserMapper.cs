using AutoMapper;
using Kernel.Toolkit.Extensions;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Infrastructure.AutoMapper.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserUpdate, User>()
                .ForMember(d => d.State, x => x.MapFrom(s => s.State.ToUpper()))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name.ToTitleCase()))
                .ForMember(d => d.NameFull, x => x.MapFrom(s => s.NameFull.ToTitleCase()))
                .ForMember(d => d.City, x => x.MapFrom(s => s.City.ToTitleCase()))
                .ForMember(d => d.Address, x => x.MapFrom(s => s.Address.ToTitleCase()))
                .ForMember(d => d.AddPictureInOrder, x => x.MapFrom(s => s.AddPictureInOrder))
                .ForMember(d => d.Picture, x => x.MapFrom((s, d) => !string.IsNullOrEmpty(s.Picture) ? s.Picture.ToByteArray() : d.Picture));

            CreateMap<User, UserResponse>()
                .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name))
                .ForMember(d => d.NameFull, x => x.MapFrom(s => s.NameFull))
                .ForMember(d => d.Address, x => x.MapFrom(s => s.Address))
                .ForMember(d => d.City, x => x.MapFrom(s => s.City))
                .ForMember(d => d.State, x => x.MapFrom(s => s.State))
                .ForMember(d => d.Cellphone, x => x.MapFrom(s => s.Cellphone))
                .ForMember(d => d.Telephone, x => x.MapFrom(s => s.Telephone))
                .ForMember(d => d.PrivacyPolicyAccepted, x => x.MapFrom(s => s.PrivacyPolicyAccepted))
                .ForMember(d => d.PushNotificationToken, x => x.MapFrom(s => s.PushNotificationToken))
                .ForMember(d => d.AddPictureInOrder, x => x.MapFrom(s => s.AddPictureInOrder))
                .ForMember(d => d.Email, x => x.MapFrom(s => s.Email))
                .ForMember(d => d.Document, x => x.MapFrom(s => s.Document))
                .ForMember(d => d.Roles, x => x.MapFrom(s => s.Roles));

            CreateMap<NotificationResponse, UserNotificationResponse>()
                .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Title, x => x.MapFrom(s => s.Title))
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Body))
                .ForMember(d => d.CreatedAt, x => x.MapFrom(s => s.Inserted))
                .ForMember(d => d.IsUnRead, x => x.MapFrom(s => s.State == NotificationState.New))
                .AfterMap((s, d) => d.Icon = "/assets/icons/ic_notification.svg");
        }
    }
}