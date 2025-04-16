using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class UserService(IGameShelfDbContext dbContext, IMapper mapper) : IUserService
    {
        public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<User> users = await dbContext.Users.ToListAsync(cancellationToken);
            return mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            User? user = await dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
            return user == null ? null : mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> PromoteToAdminAsync(Guid id, CancellationToken cancellationToken = default)
        {
            User? user = await dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user == null) throw new Exception("Utilisateur introuvable.");

            user.PromoteToAdmin();
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> DemoteToUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            User? user = await dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
            if (user == null) throw new Exception("Utilisateur introuvable.");

            user.DemoteToUser();
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserDto>(user);
        }
    }
}