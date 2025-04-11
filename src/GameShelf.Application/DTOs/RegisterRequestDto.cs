namespace GameShelf.Application.DTOs
{
    public class RegisterRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}