using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public bool InserNewLog(string message)
        {
            var log = new Log { Message = message };
            log.SetInsertedDate();
            return _logRepository.Insert(log);
        }
    }
}