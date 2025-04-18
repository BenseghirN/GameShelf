using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class PlatformService(IGameShelfDbContext dbContext, IMapper mapper) : IPlatformService
    {
        public async Task<PlatformDto> CreateAsync(NewPlatformDto dto, CancellationToken cancellationToken = default)
        {
            Platform platform = new Platform();
            platform.CreateNew(dto.Nom, dto.ImagePath);
            await dbContext.Platforms.AddAsync(platform, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<PlatformDto>(platform);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Platform? entity = await dbContext.Platforms.FindAsync(new object?[] { id }, cancellationToken);
            if (entity == null) return;
            dbContext.Platforms.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<PlatformDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<Platform> platforms = await dbContext.Platforms.ToListAsync(cancellationToken);
            return mapper.Map<List<PlatformDto>>(platforms);
        }

        public async Task<PlatformDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Platform? platform = await dbContext.Platforms.FindAsync(new object?[] { id }, cancellationToken);
            return platform == null ? null : mapper.Map<PlatformDto>(platform);
        }

        public async Task<PlatformDto> UpdateAsync(Guid id, NewPlatformDto dto, CancellationToken cancellationToken = default)
        {
            Platform? platform = await dbContext.Platforms.FindAsync(new object?[] { id }, cancellationToken);
            if (platform == null) throw new KeyNotFoundException("Platform not found");
            platform.Update(dto.Nom, dto.ImagePath);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<PlatformDto>(platform);
        }
    }
}