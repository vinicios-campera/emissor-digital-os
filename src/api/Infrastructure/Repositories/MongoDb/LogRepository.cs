using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class LogRepository : ILogRepository
    {
        private readonly OrderServiceDb _database;

        public LogRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => throw new NotImplementedException();

        public IEnumerable<Log>? Find(Expression<Func<Log, bool>> expression) => throw new NotImplementedException();

        public Log? FindById(Guid id) => throw new NotImplementedException();

        public bool Insert(Log entity)
        {
            _database.Logs.InsertOne(entity);
            return true;
        }

        public bool Update(Log entity, bool isUpsert = false) => throw new NotImplementedException();
    }
}