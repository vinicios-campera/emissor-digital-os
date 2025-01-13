using System;

namespace OrderService.Models.Order
{
    public class OrderResponseCustom
    {
        public Guid Id { get; set; }
        public DateTime DateInsert { get; set; }
        public int Identifier { get; set; }
        public string? Client { get; set; }
        public bool Pay { get; set; }
        public bool IsChecked { get; set; }
        public double Amount { get; set; }
    }
}