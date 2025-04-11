using GameShelf.Domain.Entities;

namespace GameShelf.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}