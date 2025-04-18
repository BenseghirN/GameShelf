using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class TagService(IGameShelfDbContext dbContext, IMapper mapper) : ITagService
    {
        public async Task<TagDto> CreateAsync(NewTagDto dto, CancellationToken cancellationToken = default)
        {
            Tag tag = new Tag();
            tag.CreateNew(dto.Nom);
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<TagDto>(tag);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Tag? tag = await dbContext.Tags.FindAsync(new object[] { id }, cancellationToken);
            if (tag == null) throw new Exception("Tag introuvable.");

            dbContext.Tags.Remove(tag);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<Tag> tags = await dbContext.Tags.ToListAsync(cancellationToken);
            return mapper.Map<List<TagDto>>(tags);
        }

        public async Task<TagDto> UpdateAsync(Guid id, NewTagDto dto, CancellationToken cancellationToken = default)
        {
            Tag? tag = await dbContext.Tags.FindAsync(new object[] { id }, cancellationToken);
            if (tag == null) throw new Exception("Tag introuvable.");
            tag.Update(dto.Nom);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<TagDto>(tag);
        }
    }
}