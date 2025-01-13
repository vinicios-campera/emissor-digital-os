using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Response
{
    public class ClientResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Cep { get; set; }
        public string? Cellphone { get; set; }
        public Person Type { get; set; }
        public DateTime Inserted { get; private set; }
    }
}