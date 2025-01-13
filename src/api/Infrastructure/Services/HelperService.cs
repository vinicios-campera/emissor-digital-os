using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Services
{
    public class HelperService : IHelperService
    {
        private readonly IMessageRepository _messageRepository;

        public HelperService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public bool AddMessage(MessageInsert payload)
        {
            var message = new Message { Description = payload.Description, Email = payload.Email, Name = payload.Name };
            message.SetInsertedDate();
            return _messageRepository.Insert(message);
        }

        public string GetDownloadApp() => "https://drive.google.com/file/d/1PVGJHSjVDrqDkmmEfyTrammRW-0Lk8AT/view?usp=drive_link";
    }
}