using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserProposalService
    {
        Task<List<UserProposalDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserProposalDto> CreateAsync(NewProposalDto dto, CancellationToken cancellationToken = default);
        Task AcceptAsync(Guid proposalId, string description, List<Guid> tags, CancellationToken cancellationToken = default);
        Task RejectAsync(Guid proposalId, CancellationToken cancellationToken = default);
    }
}