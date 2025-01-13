using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Domain.Enums;
using Kernel.Data.Repository.Entity;
using Kernel.Toolkit.Extensions;

namespace OrderService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Client : EntityBase
    {
        public Guid Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Cep { get; set; }
        public string? Cellphone { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Person Type
        {
            get
            {
                if(string.IsNullOrEmpty(Document))
                    return Person.Unknown;

                if (Document!.IsPhysicalDocument())
                    return Person.Physical;

                if (Document!.IsLegalDocument())
                    return Person.Legal;

                throw new Exception($"{nameof(Document)} format is invalid");
            }
        }
    }
}