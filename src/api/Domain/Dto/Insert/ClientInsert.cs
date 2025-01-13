namespace OrderService.Domain.Dto.Insert
{
    public class ClientInsert
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Cep { get; set; }
        public string? Cellphone { get; set; }
    }
}