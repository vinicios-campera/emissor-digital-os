using OrderService.Domain.Dto.Insert;

namespace OrderService.Application.Interfaces.Services
{
    public interface IHelperService
    {
        string GetDownloadApp();

        bool AddMessage(MessageInsert payload);
    }
}