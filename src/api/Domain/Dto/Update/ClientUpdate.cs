namespace OrderService.Domain.Dto.Update
{
    public class ClientUpdate
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Cep { get; set; }
        public string? Cellphone { get; set; }
    }
}