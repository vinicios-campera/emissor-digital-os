using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly OrderServiceDb _database;

        public NotificationRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => throw new NotImplementedException();

        public IEnumerable<Notification>? Find(Expression<Func<Notification, bool>> expression) => _database.Notifications.AsQueryable().Where(expression).ToList();

        public Notification? FindById(Guid id) => _database.Notifications.AsQueryable().FirstOrDefault(x => x.Id == id);

        public bool Insert(Notification entity)
        {
            _database.Notifications.InsertOne(entity);
            return true;
        }

        public bool Update(Notification entity, bool isUpsert = false) => _database.Notifications.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = isUpsert }).ModifiedCount > 0;
    }
}