using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class ClientRepository : IClientRepository
    {
        private readonly OrderServiceDb _database;

        public ClientRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => _database.Clients.DeleteOne(x => x.Id == id).DeletedCount > 0;

        public IEnumerable<Client>? Find(Expression<Func<Client, bool>> expression) => _database.Clients.AsQueryable().Where(expression).ToList();

        public Client? FindById(Guid id) => _database.Clients.AsQueryable().FirstOrDefault(x => x.Id == id);

        public bool Insert(Client entity)
        {
            _database.Clients.InsertOne(entity);
            return true;
        }

        public bool Update(Client entity, bool isUpsert = false) => _database.Clients.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = isUpsert }).ModifiedCount > 0;
    }
}