using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class MessageRepository : IMessageRepository
    {
        private readonly OrderServiceDb _database;

        public MessageRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => throw new NotImplementedException();

        public IEnumerable<Message>? Find(Expression<Func<Message, bool>> expression) => throw new NotImplementedException();

        public Message? FindById(Guid id) => throw new NotImplementedException();

        public bool Insert(Message entity)
        {
            _database.Messages.InsertOne(entity);
            return true;
        }

        public bool Update(Message entity, bool isUpsert = false) => throw new NotImplementedException();
    }
}