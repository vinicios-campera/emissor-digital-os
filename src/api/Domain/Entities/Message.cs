using MongoDB.Bson.Serialization.Attributes;
using Kernel.Data.Repository.Entity;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Message : EntityBase
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
    }
}