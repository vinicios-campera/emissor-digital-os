using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Domain.Enums;
using Kernel.Data.Repository.Entity;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Order : EntityBase
    {
        public Guid Id { get; set; }
        public string? UserEmail { get; set; }
        public Guid ClientId { get; set; }
        public int Identifier { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string? Note { get; set; }

        [BsonRepresentation(BsonType.String)]
        public OrderState State { get; set; }

        public byte[]? Pdf { get; set; }
    }
}