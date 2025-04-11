using GameShelf.Domain.Entities;

namespace GameShelf.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task AddAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}