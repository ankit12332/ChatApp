namespace ChatApp.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        // Password is intentionally omitted to not expose it through APIs
        public DateTime CreatedAt { get; set; }
    }
}
