using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class UserProposalService(IGameShelfDbContext dbContext, IMapper mapper, IUserSynchronizationService userSynchronizationService) : IUserProposalService
    {
        public async Task<GameDto> AcceptAsync(Guid proposalId, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals
                .Include(p => p.Platform)
                .FirstOrDefaultAsync(p => p.Id == proposalId, cancellationToken);
            if (proposal == null) throw new KeyNotFoundException("Proposition introuvable");

            Game newGame = new Game();
            newGame.Titre = proposal.Titre;

            newGame.AddPlatforms(new[] { proposal.Platform });

            await dbContext.Games.AddAsync(newGame, cancellationToken);
            proposal.Validate();
            await dbContext.SaveChangesAsync(cancellationToken);

            return mapper.Map<GameDto>(newGame);
        }

        public async Task<UserProposalDto> CreateAsync(NewProposalDto dto, CancellationToken cancellationToken = default)
        {
            User? user = await userSynchronizationService.EnsureUserExistsAsync() ?? throw new UnauthorizedAccessException();
            bool exists = await dbContext.Platforms.AnyAsync(p => p.Id == dto.PlatformId);
            if (!exists)
                throw new ArgumentException("La plateforme spécifiée n'existe pas.");

            UserProposal proposal = user.ProposeGame(userId: user.Id, platformId: dto.PlatformId, titre: dto.Titre);
            await dbContext.UserProposals.AddAsync(proposal);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserProposalDto>(proposal);
        }

        public async Task DeleteProposalAsync(Guid proposalId, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals.FindAsync(new object[] { proposalId }, cancellationToken);
            if (proposal == null) throw new Exception("Proposition introuvable.");

            dbContext.UserProposals.Remove(proposal);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserProposalDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<UserProposal> proposals = await dbContext.UserProposals
                .Include(p => p.Platform)
                .ToListAsync(cancellationToken);
            return mapper.Map<List<UserProposalDto>>(proposals);
        }

        public async Task<UserProposalDto> GetByIdAsync(Guid proposalId, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals
                .Include(p => p.Platform)
                .FirstOrDefaultAsync(p => p.Id == proposalId, cancellationToken);
            if (proposal == null) throw new KeyNotFoundException("Proposition introuvable");
            return mapper.Map<UserProposalDto>(proposal);
        }

        public async Task<List<UserProposalDto>> GetProposalsForCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            User? user = await userSynchronizationService.EnsureUserExistsAsync() ?? throw new UnauthorizedAccessException();
            List<UserProposal> proposals = await dbContext.UserProposals
                .Include(p => p.Platform)
                .Where(p => p.UserId == user.Id)
                .ToListAsync(cancellationToken);
            return mapper.Map<List<UserProposalDto>>(proposals);
        }

        public async Task RejectAsync(Guid proposalId, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals.FindAsync(new object?[] { proposalId }, cancellationToken);
            if (proposal == null) throw new KeyNotFoundException("Proposition introuvable");

            proposal.Reject();
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserProposalDto> UpdateProposalInfoAsync(Guid proposalId, NewProposalDto dto, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals.FindAsync(new object?[] { proposalId }, cancellationToken);
            if (proposal == null) throw new KeyNotFoundException("Proposition introuvable");

            bool exists = await dbContext.Platforms.AnyAsync(p => p.Id == dto.PlatformId, cancellationToken);
            if (!exists)
                throw new ArgumentException("La plateforme spécifiée n'existe pas.");

            proposal.UpdateInfo(dto.Titre, dto.PlatformId);
            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserProposalDto>(proposal);
        }
    }
}