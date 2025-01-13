using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderServiceDb _database;

        public OrderRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => _database.Orders.DeleteOne(x => x.Id == id).DeletedCount > 0;

        public IEnumerable<Order>? Find(Expression<Func<Order, bool>> expression) => _database.Orders.AsQueryable().Where(expression).ToList();

        public Order? FindById(Guid id) => _database.Orders.AsQueryable().FirstOrDefault(x => x.Id == id);

        public bool Insert(Order entity)
        {
            _database.Orders.InsertOne(entity);
            return true;
        }

        public bool Update(Order entity, bool isUpsert = false) => _database.Orders.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = isUpsert }).ModifiedCount > 0;
    }
}