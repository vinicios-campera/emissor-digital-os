using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Domain.Enums;
using Kernel.Data.Repository.Entity;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Product : EntityBase
    {
        public Guid Id { get; set; }
        public string? UserEmail { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Measure Measure { get; set; }

        public decimal UnitaryValue { get; set; }
        public string? Description { get; set; }
    }
}