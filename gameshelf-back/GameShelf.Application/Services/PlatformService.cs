using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class PlatformService(IGameShelfDbContext dbContext, IMapper mapper) : IPlatformService
    {
        public async Task<PlatformDto> CreateAsync(PlatformDto dto, CancellationToken cancellationToken = default)
        {
            Platform platform = mapper.Map<Platform>(dto);
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

        public async Task<PlatformDto> UpdateAsync(Guid id, PlatformDto dto, CancellationToken cancellationToken = default)
        {
            Platform? entity = await dbContext.Platforms.FindAsync(new object?[] { id }, cancellationToken);
            if (entity == null) throw new KeyNotFoundException("Platform not found");

            mapper.Map(dto, entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<PlatformDto>(entity);
        }
    }
}