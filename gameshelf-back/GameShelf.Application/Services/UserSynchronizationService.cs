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
            User? user = null;
            try
            {
                UserDto currentUser = authService.CurrentUser;
                if (currentUser == null)
                    throw new UnauthorizedAccessException("Aucun utilisateur connecté");

                user = await dbContext.Users.FirstOrDefaultAsync(u => u.ExternalId == currentUser.ExternalId, cancellationToken);
                if (user != null)
                    return user;

                user = new User();
                user.Create(
                    currentUser.ExternalId,
                    currentUser.Email,
                    currentUser.Pseudo,
                    currentUser.GivenName,
                    currentUser.Surname
                );
                await dbContext.Users.AddAsync(user, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return await dbContext.Users
                    .FirstOrDefaultAsync(u => u.ExternalId == user!.ExternalId, cancellationToken)
                    ?? throw new InvalidOperationException("User could not be found after unique constraint violation.");
            }
        }

        public async Task<UserDto> GetCurrentUserInfosAsync(CancellationToken cancellationToken = default)
        {
            User user = await EnsureUserExistsAsync(cancellationToken);
            return mapper.Map<UserDto>(user);
        }

        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex.InnerException?.Message.Contains("UNIQUE") == true;
        }
    }
}