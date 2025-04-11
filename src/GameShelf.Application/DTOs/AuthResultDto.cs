namespace GameShelf.Application.DTOs
{
    public class AuthResultDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}