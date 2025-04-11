namespace GameShelf.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}