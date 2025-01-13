using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Insert
{
    public class ProductInsert
    {
        public Measure Measure { get; set; }
        public decimal UnitaryValue { get; set; }
        public string? Description { get; set; }
    }
}