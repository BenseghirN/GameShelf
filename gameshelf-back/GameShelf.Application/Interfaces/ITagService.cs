using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TagDto> CreateAsync(NewTagDto dto, CancellationToken cancellationToken = default);
        Task<TagDto> UpdateAsync(Guid id, NewTagDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}