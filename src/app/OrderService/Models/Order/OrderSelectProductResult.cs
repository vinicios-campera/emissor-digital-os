using OrderService.Models.Item;

namespace OrderService.Models.Order
{
    public class OrderSelectProductResult
    {
        public ItemResponseCustom? Item { get; set; }
        public double Amount { get; set; }
    }
}