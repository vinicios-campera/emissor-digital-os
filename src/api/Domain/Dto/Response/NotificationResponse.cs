using OrderService.Domain.Enums;

namespace OrderService.Domain.Dto.Response
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Error { get; set; }
        public DateTime Inserted { get; set; }
        public NotificationState State { get; set; }
    }
}