using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public ClientResponse? Client { get; set; }
        public int Identifier { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string? Note { get; set; }
        public OrderState State { get; set; }
        public DateTime Inserted { get; set; }
    }

    public class OrderPdfResponse
    {
        public byte[]? Pdf { get; set; }
        public string? FileName { get; set; }
    }

    public class OrderDetailResponse
    {
        public Guid Id { get; set; }
        public int Identifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime Inserted { get; set; }
        public string? PdfUrl { get; set; }
    }
}