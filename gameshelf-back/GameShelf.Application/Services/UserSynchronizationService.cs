using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class UserSynchronizationService(IAuthService authService, IGameShelfDbContext dbContext, IMapper mapper) : IUserSynchronizationService
    {
        public async Task<User> EnsureUserExistsAsync(CancellationToken cancellationToken = default)
        {
            UserDto currentUser = authService.CurrentUser;
            if (currentUser == null)
                throw new UnauthorizedAccessException("Aucun utilisateur connecté");

            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.ExternalId == currentUser.ExternalId, cancellationToken);
            if (user != null)
                return user;

            user = User.Create(
                currentUser.ExternalId,
                currentUser.Email,
                currentUser.Pseudo,
                currentUser.GivenName,
                currentUser.Surname
            );
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<UserDto> GetCurrentUserInfosAsync(CancellationToken cancellationToken = default)
        {
            User user = await EnsureUserExistsAsync(cancellationToken);
            return mapper.Map<UserDto>(user);
        }
    }
}