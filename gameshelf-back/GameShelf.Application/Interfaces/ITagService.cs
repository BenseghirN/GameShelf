using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TagDto> CreateAsync(TagDto dto, CancellationToken cancellationToken = default);
        Task<TagDto> UpdateAsync(int id, TagDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}