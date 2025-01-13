namespace OrderService.Models.Firebase
{
    public class OpenId
    {
        public string? Account { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Scope { get; set; }
        public string? Authority { get; set; }
        public string? RedirectUrl { get; set; }
    }
}