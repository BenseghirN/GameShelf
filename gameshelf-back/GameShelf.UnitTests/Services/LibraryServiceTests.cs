﻿using AutoMapper;
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
            DbContextOptions<GameShelfDbContext> options = new DbContextOptionsBuilder<GameShelfDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new GameShelfDbContext(options);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            User user = new User
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
                        Statut = GameStatus.Termine,
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

            Mock<IUserSynchronizationService> mockSync = new Mock<IUserSynchronizationService>();
            mockSync.Setup(s => s.EnsureUserExistsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _libraryService = new LibraryService(_dbContext, _mapper, mockSync.Object);
        }

        [Fact]
        public async Task AddGameToLibrary_ShouldSucceed()
        {
            Guid gameId = Guid.NewGuid();
            UserGameDto result = await _libraryService.AddGameToLibraryAsync(gameId, GameStatus.Possede, 4, null);

            result.Should().NotBeNull();
            result.UserId.Should().Be(_userId);
            result.GameId.Should().Be(gameId);
            result.Statut.Should().Be(GameStatus.Possede);
            result.Note.Should().Be(4);
        }

        [Fact]
        public async Task GetUserLibrary_ShouldReturnGames()
        {
            // Act
            List<UserGameDto> library = await _libraryService.GetUserLibraryAsync();

            // Assert
            library.Should().NotBeNull();
            library.Should().HaveCount(2);
        }

        [Fact]
        public async Task RemoveGameFromLibrary_ShouldSucceed()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            User? user = await _dbContext.Users.FindAsync(_userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possede, Note = 4 }
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
            Guid gameId = Guid.NewGuid();

            User? user = await _dbContext.Users.FindAsync(_userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possede, Note = 4 }
            };
            await _dbContext.SaveChangesAsync();

            // Act
            UserGameDto updatedGame = await _libraryService.UpdateGameStatusAsync(gameId, GameStatus.Termine, 5);

            // Assert
            updatedGame.Should().NotBeNull();
            updatedGame.GameId.Should().Be(gameId);
            updatedGame.Statut.Should().Be(GameStatus.Termine);
            updatedGame.Note.Should().Be(5);
        }

        [Fact]
        public async Task AddGameToLibrary_ShouldNotAddDuplicateGame()
        {
            // Arrange
            Guid gameId = Guid.NewGuid();

            User? user = await _dbContext.Users.FindAsync(_userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            user.UserGames = new List<UserGame>
            {
                new UserGame { GameId = gameId, Statut = GameStatus.Possede, Note = 4 }
            };
            await _dbContext.SaveChangesAsync();

            // Act
            Func<Task> act = async () => await _libraryService.AddGameToLibraryAsync(gameId, GameStatus.Possede, 4, null);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Ce jeu est déjà présent dans la collection.");
        }

        [Fact]
        public async Task GetLibraryStats_ShouldReturnCorrectCounts()
        {
            // Act
            StatsDto stats = await _libraryService.GetLibraryStatsAsync();

            // Assert
            stats.Should().NotBeNull();
            stats.NbTotalGames.Should().Be(2);
            stats.NbOngoingGames.Should().Be(1);
        }

    }
}
