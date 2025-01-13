using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Services
{
    public interface IUserService
    {
        bool AcceptPrivacyPolicy();

        UserResponse? GetUser();

        bool UpdateUser(UserUpdate payload);

        UserResponse? GetUserLogged();

        User? GetUserDatabaseLogged();

        UserResponse? GetUserById(Guid id);

        bool UpdatePushNotificationToken(string token);

        bool DeleteAccount();

        bool UpdateAddPictureInOrder(bool insert);

        byte[]? GetPictureByUserId(Guid id);
    }
}