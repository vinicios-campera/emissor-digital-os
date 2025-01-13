using Kernel.Data.Repository.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class User : EntityBase
    {
        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameFull { get; set; }
        public string? Address { get; set; }
        public string? Document { get; set; }
        public string? City { get; set; }
        public string? Telephone { get; set; }
        public string? State { get; set; }
        public string? Cellphone { get; set; }
        public byte[]? Picture { get; set; }
        public string? Email { get; set; }
        public bool PrivacyPolicyAccepted { get; set; }
        public string? PushNotificationToken { get; set; }
        public DateTime? LastLogin { get; private set; }
        public bool AddPictureInOrder { get; set; }
        public IEnumerable<string>? Roles { get; set; }

        public void SetILastLoginDate() => LastLogin = DateTime.Now;
    }
}