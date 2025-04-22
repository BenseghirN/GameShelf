using FluentAssertions;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;

namespace GameShelf.UnitTests.Services
{
    public class UserTests
    {
        [Fact]
        public void AddGame_ShouldAddNewGame()
        {
            User user = new User { Id = Guid.NewGuid() };
            Guid gameId = Guid.NewGuid();

            user.AddGame(gameId, GameStatus.Possede, 5);

            user.UserGames.Should().ContainSingle(ug => ug.GameId == gameId);
        }

        [Fact]
        public void AddGame_ShouldThrow_WhenGameAlreadyExists()
        {
            User user = new User { Id = Guid.NewGuid() };
            Guid gameId = Guid.NewGuid();
            user.AddGame(gameId);

            Action act = () => user.AddGame(gameId);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RemoveGame_ShouldRemoveGameFromCollection()
        {
            User user = new User { Id = Guid.NewGuid() };
            Guid gameId = Guid.NewGuid();
            user.AddGame(gameId);

            user.RemoveGame(gameId);

            user.UserGames.Should().BeEmpty();
        }

        [Fact]
        public void PromoteToAdmin_ShouldSetRoleToAdmin()
        {
            var user = new User();
            user.PromoteToAdmin();

            user.Role.Should().Be(UserRole.Admin);
        }

        [Fact]
        public void UpdateGameStatus_ShouldUpdateNoteAndStatut()
        {
            var user = new User { Id = Guid.NewGuid() };
            var gameId = Guid.NewGuid();
            user.AddGame(gameId, GameStatus.EnCours, 4);

            user.UpdateGameStatus(gameId, GameStatus.Termine, 5);

            var updated = user.UserGames.First();
            updated.Statut.Should().Be(GameStatus.Termine);
            updated.Note.Should().Be(5);
        }

        [Fact]
        public void ProposeGame_ShouldAddNewProposal()
        {
            var user = new User { Id = Guid.NewGuid() };
            var platformId = Guid.NewGuid();
            string titre = "Zelda";

            var proposal = user.ProposeGame(user.Id, platformId, titre, "image.jpg");

            user.Propositions.Should().ContainSingle();
            proposal.Titre.Should().Be("Zelda");
            proposal.PlatformId.Should().Be(platformId);
            proposal.Statut.Should().Be(ProposalStatus.EnAttente);
        }

    }
}
