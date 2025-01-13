using MongoDB.Bson.Serialization.Attributes;
using Kernel.Data.Repository.Entity;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Log : EntityBase
    {
        public Guid Id { get; set; }
        public string? Message { get; set; }
    }
}