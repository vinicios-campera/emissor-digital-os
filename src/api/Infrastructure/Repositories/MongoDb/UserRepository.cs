using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderServiceDb _database;

        public UserRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => _database.Users.DeleteOne(x => x.Id == id).DeletedCount > 0;

        public IEnumerable<User>? Find(Expression<Func<User, bool>> expression) => _database.Users.AsQueryable().Where(expression).ToList();

        public User? FindByEmail(string email) => _database.Users.AsQueryable().FirstOrDefault(x => x.Email == email);

        public User? FindById(Guid id) => _database.Users.AsQueryable().FirstOrDefault(x => x.Id == id);

        public bool Insert(User entity)
        {
            _database.Users.InsertOne(entity);
            return true;
        }

        public bool Update(User entity, bool isUpsert = false) => _database.Users.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = isUpsert }).ModifiedCount > 0;
    }
}