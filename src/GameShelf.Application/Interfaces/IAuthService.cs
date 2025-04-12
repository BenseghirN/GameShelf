using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IAuthService
    {
        UserDto CurrentUser { get; }
    }
}
