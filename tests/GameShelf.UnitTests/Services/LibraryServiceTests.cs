using AutoMapper;
using FluentAssertions;
using GameShelf.Application.DTOs;
using GameShelf.Application.Interfaces;
using GameShelf.Application.Mapping;
using GameShelf.Application.Services;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GameShelf.UnitTests.Services
{
    public class LibraryServiceTests
    {
        private readonly GameShelfDbContext _dbContext;
        private readonly LibraryService _libraryService;
        private readonly IMapper _mapper;
        private readonly Guid _userId = Guid.NewGuid();
        private readonly Guid _gameId1 = Guid.NewGuid();
        private readonly Guid _gameId2 = Guid.NewGuid();

        public LibraryServiceTests()
        {
            var options = new DbContextOptionsBuilder<GameShelfDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new GameShelfDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            var user = new User
            {
                Id = _userId,
                ExternalId = "external-id-001",
                Email = "test@example.com",
                Pseudo = "TestUser",
                UserGames = new List<UserGame>
                {
                    new UserGame
                    {
                        UserId = _userId,
                        GameId = _gameId1,
                        Game = new Game { Id = _gameId1, Titre = "Zelda" },
                        Statut = GameStatus.Terminé,
                        Note = 5,
                        DateAjout = DateTime.UtcNow
                    },
                    new UserGame
                    {
                        UserId = _userId,
                        GameId = _gameId2,
                        Game = new Game { Id = _gameId2, Titre = "Mario" },
                        Statut = GameStatus.EnCours,
                        Note = 3,
                        DateAjout = DateTime.UtcNow
                    }
                }
            };

            _dbContext.Users.Add(user);
            _dbContext.Games.AddRange(user.UserGames.Select(ug => ug.Game));
            _dbContext.SaveChanges();

            var mockSync = new Moq.Mock<IUserSynchronizationService>();
            mockSync.Setup(s => s.EnsureUserExistsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _libraryService = new LibraryService(_dbContext, _mapper, mockSync.Object);
        }

        [Fact]
        public async Task AddGameToLibrary_ShouldSucceed()
        {
            Guid gameId = Guid.NewGuid();
            var result = await _libraryService.AddGameToLibraryAsync(gameId, GameStatus.Possédé, 4, null);

            result.Should().NotBeNull();
            result.UserId.Should().Be(_userId);
            result.GameId.Should().Be(gameId);
            result.Statut.Should().Be(GameStatus.Possédé);
            result.Note.Should().Be(4);
        }

        [Fact]
        public async Task GetUserLibrary_ShouldReturnGames()
        {
            //// Arrange
            //var gameId1 = Guid.NewGuid();
            //var gameId2 = Guid.NewGuid();
            //var game1 = new Game { Id = gameId1, Titre = "Zelda" };
            //var game2 = new Game { Id = gameId2, Titre = "Mario" };
            //await _dbContext.Games.AddRangeAsync(game1, game2);

            //var user = await _dbContext.Users.FindAsync(_userId);
            //user.UserGames = new List<UserGame>
            //{
            //    new UserGame { GameId = gameId1, Statut = GameStatus.Terminé, Note = 5 },
            //    new UserGame { GameId = gameId2, Statut = GameStatus.EnCours, Note = 3 }
            //};

            //await _dbContext.SaveChangesAsync();

            // Act
            var library = await _libraryService.GetUserLibraryAsync();

            // Assert
            library.Should().NotBeNull();
            library.Should().HaveCount(2);
            //library.First().GameId.Should().Be(gameId1);
            //library.Last().Statut.Should().Be(GameStatus.EnCours);
        }

        [Fact]
        public async Task RemoveGameFromLibrary_ShouldSucceed()
        {
            // Arrange
            var gameId = Guid.NewGuid();

            var user = await _dbContext.Users.FindAsync(_userId);
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possédé, Note = 4 }
            };
            await _dbContext.SaveChangesAsync();

            // Act
            await _libraryService.RemoveGameFromLibraryAsync(gameId);

            // Assert
            user.UserGames.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateGameStatus_ShouldUpdateCorrectly()
        {
            // Arrange
            var gameId = Guid.NewGuid();

            var user = await _dbContext.Users.FindAsync(_userId);
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possédé, Note = 4 }
            };
            await _dbContext.SaveChangesAsync();

            // Act
            var updatedGame = await _libraryService.UpdateGameStatusAsync(gameId, GameStatus.Terminé, 5);

            // Assert
            updatedGame.Should().NotBeNull();
            updatedGame.GameId.Should().Be(gameId);
            updatedGame.Statut.Should().Be(GameStatus.Terminé);
            updatedGame.Note.Should().Be(5);
        }

        [Fact]
        public async Task AddGameToLibrary_ShouldNotAddDuplicateGame()
        {
            // Arrange
            var gameId = Guid.NewGuid();

            var user = await _dbContext.Users.FindAsync(_userId);
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possédé, Note = 4 }
            };
            await _dbContext.SaveChangesAsync();

            // Act
            Func<Task> act = async () => await _libraryService.AddGameToLibraryAsync(gameId, GameStatus.Possédé, 4, null);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Ce jeu est déjà présent dans la collection.");
        }
    }
}
