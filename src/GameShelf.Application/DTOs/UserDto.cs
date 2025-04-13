using GameShelf.Domain.Enums;

namespace GameShelf.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}