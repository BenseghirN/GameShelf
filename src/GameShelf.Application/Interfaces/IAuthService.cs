using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResultDto> LoginAsync(LoginRequestDto dto);
    }
}
