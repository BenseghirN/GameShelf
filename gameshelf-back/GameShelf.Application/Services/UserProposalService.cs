using AutoMapper;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShelf.Application.Services
{
    public class UserProposalService(IGameShelfDbContext dbContext, IMapper mapper, IUserSynchronizationService userSynchronizationService) : IUserProposalService
    {
        public async Task AcceptAsync(Guid proposalId, string description, List<TagDto> tags, CancellationToken cancellationToken = default)
        {
            UserProposal? proposal = await dbContext.UserProposals
                .Include(p => p.Platform)
                .FirstOrDefaultAsync(p => p.Id == proposalId, cancellationToken);
            if (proposal == null) throw new KeyNotFoundException("Proposition introuvable");

            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Titre = proposal.Titre,
                ImagePath = proposal.ImagePath,
                Description = description
            };

            game.AddPlatforms(new[] { proposal.Platform });
            game.AddTags(mapper.Map<List<Tag>>(tags));

            dbContext.Games.Add(game);
            dbContext.UserProposals.Remove(proposal);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserProposalDto> CreateAsync(NewProposalDto dto, CancellationToken cancellationToken = default)
        {
            User? user = await userSynchronizationService.EnsureUserExistsAsync() ?? throw new UnauthorizedAccessException();
            bool exists = await dbContext.Platforms.AnyAsync(p => p.Id == dto.PlatformId);
            if (!exists)
                throw new ArgumentException("La plateforme spécifiée n'existe pas.");

            UserProposal proposal = user.ProposeGame(userId: user.Id, platformId: dto.PlatformId, titre: dto.Titre);

            await dbContext.SaveChangesAsync(cancellationToken);
            return mapper.Map<UserProposalDto>(proposal);
        }

        public async Task<List<UserProposalDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            List<UserProposal> proposals = await dbContext.UserProposals
                .Include(p => p.Platform)
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
    }
}