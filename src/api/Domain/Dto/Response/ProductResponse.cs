using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public Measure Measure { get; set; }
        public decimal UnitaryValue { get; set; }
        public string? Description { get; set; }
        public DateTime Inserted { get; private set; }
    }
}