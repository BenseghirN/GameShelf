using GameShelf.Application.DTOs;

namespace GameShelf.Application.Interfaces
{
    public interface IUserProposalService
    {
        Task<List<UserProposalDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserProposalDto> CreateAsync(NewProposalDto dto, CancellationToken cancellationToken = default);
        Task<GameDto> AcceptAsync(Guid proposalId, CancellationToken cancellationToken = default);
        Task RejectAsync(Guid proposalId, CancellationToken cancellationToken = default);
        Task<List<UserProposalDto>> GetProposalsForCurrentUserAsync(CancellationToken cancellationToken = default);
        Task DeleteProposalAsync(Guid proposalId, CancellationToken cancellationToken = default);
        Task<UserProposalDto> UpdateProposalInfoAsync(Guid proposalId, NewProposalDto dto, CancellationToken cancellationToken = default);
        Task<UserProposalDto> GetByIdAsync(Guid proposalId, CancellationToken cancellationToken = default);
    }
}