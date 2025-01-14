using Microsoft.Extensions.Configuration;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Services
{
    public class HelperService : IHelperService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConfiguration _configuration;

        public HelperService(IMessageRepository messageRepository, IConfiguration configuration)
        {
            _messageRepository = messageRepository;
            _configuration = configuration;
        }

        public bool AddMessage(MessageInsert payload)
        {
            var message = new Message { Description = payload.Description, Email = payload.Email, Name = payload.Name };
            message.SetInsertedDate();
            return _messageRepository.Insert(message);
        }

        public string GetDownloadApp() => _configuration.GetValue<string>("Urls:DownloadApk");
    }
}