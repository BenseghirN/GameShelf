namespace GameShelf.Application.DTOs
{
    public class AdminCreateUserDto
    {
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}