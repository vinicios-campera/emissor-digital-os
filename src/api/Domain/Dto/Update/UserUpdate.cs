namespace OrderService.Domain.Dto.Update
{
    public class UserUpdate
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NameFull { get; set; }
        public string? Address { get; set; }
        public string? Document { get; set; }
        public string? City { get; set; }
        public string? Telephone { get; set; }
        public string? State { get; set; }
        public string? Cellphone { get; set; }
        public string? Picture { get; set; }
        public bool AddPictureInOrder { get; set; }
    }
}