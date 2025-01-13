using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.Data;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Infrastructure.Repositories.MongoDb
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderServiceDb _database;

        public ProductRepository(OrderServiceDb database) => _database = database;

        public bool Delete(Guid id) => _database.Products.DeleteOne(x => x.Id == id).DeletedCount > 0;

        public IEnumerable<Product>? Find(Expression<Func<Product, bool>> expression) => _database.Products.AsQueryable().Where(expression).ToList();

        public Product? FindById(Guid id) => _database.Products.AsQueryable().FirstOrDefault(x => x.Id == id);

        public bool Insert(Product entity)
        {
            _database.Products.InsertOne(entity);
            return true;
        }

        public bool Insert(IEnumerable<Product> entities)
        {
            _database.Products.InsertMany(entities);
            return true;
        }

        public bool Update(Product entity, bool isUpsert = false) => _database.Products.ReplaceOne(x => x.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = isUpsert }).ModifiedCount > 0;
    }
}