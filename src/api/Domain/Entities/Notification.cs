using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Domain.Enums;
using Kernel.Data.Repository.Entity;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Notification : EntityBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Error { get; set; }

        [BsonRepresentation(BsonType.String)]
        public NotificationState State { get; set; }
    }
}