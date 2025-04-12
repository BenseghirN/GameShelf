using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto> AddAsync(AdminCreateUserDto dto);
        Task DeleteAsync(Guid id);
    }
}