using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Update
{
    public class ProductUpdate
    {
        public Guid Id { get; set; }
        public Measure Measure { get; set; }
        public decimal UnitaryValue { get; set; }
        public string? Description { get; set; }
    }
}