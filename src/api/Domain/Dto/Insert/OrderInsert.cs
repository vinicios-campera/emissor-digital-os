using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Insert
{
    public class OrderInsert
    {
        public Guid? ClientId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string? Note { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<OrderProductInsert>? Products { get; set; }
    }

    public class OrderProductInsert
    {
        public Guid? Id { get; set; }
        public Measure Measure { get; set; }
        public decimal UnitaryValue { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public bool IsNew { get => Id == null; }
        public decimal Subtotal { get => UnitaryValue * Amount; }

    }
}