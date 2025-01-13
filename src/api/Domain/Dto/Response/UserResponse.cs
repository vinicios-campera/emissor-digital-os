namespace OrderService.Domain.Dto.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public bool PrivacyPolicyAccepted { get; set; }
        public string? PushNotificationToken { get; set; }
        public bool AddPictureInOrder { get; set; }
        public string? Name { get; set; }
        public string? NameFull { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Document { get; set; }
        public string? City { get; set; }
        public string? Telephone { get; set; }
        public string? State { get; set; }
        public string? Cellphone { get; set; }
        public string? PictureUrl { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public IEnumerable<UserNotificationResponse>? Notifications { get; set; }
    }

    public class UserNotificationResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUnRead { get; set; }
    }
}