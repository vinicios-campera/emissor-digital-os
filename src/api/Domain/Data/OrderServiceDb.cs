using MongoDB.Driver;
using OrderService.Domain.Entities;
using Kernel.Data.MongoDb;

namespace OrderService.Domain.Data
{
    public class OrderServiceDb : MongoDbBase
    {
        public IMongoCollection<Log> Logs => GetEntity<Log>(nameof(Log));
        public IMongoCollection<Client> Clients => GetEntity<Client>(nameof(Client));
        public IMongoCollection<Notification> Notifications => GetEntity<Notification>(nameof(Notification));
        public IMongoCollection<Order> Orders => GetEntity<Order>(nameof(Order));
        public IMongoCollection<Product> Products => GetEntity<Product>(nameof(Product));
        public IMongoCollection<User> Users => GetEntity<User>(nameof(User));
        public IMongoCollection<Message> Messages => GetEntity<Message>(nameof(Message));
    }
}