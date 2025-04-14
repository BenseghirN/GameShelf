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

            user.AddGame(gameId, GameStatus.Possédé, 5);

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
    }
}
