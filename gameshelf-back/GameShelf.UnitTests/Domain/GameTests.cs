using FluentAssertions;
using GameShelf.Domain.Entities;

namespace GameShelf.UnitTests.Services
{
    public class GameTests
    {
        [Fact]
        public void AddTags_ShouldAddTagsIfNotAlreadyPresent()
        {
            Guid tagId = Guid.NewGuid();
            Game game = new Game { Id = Guid.NewGuid(), Titre = "Test Game" };
            List<Tag> tags = new List<Tag> { new Tag { Id = tagId, Nom = "RPG" } };

            game.AddTags(tags);

            game.GameTags.Should().ContainSingle(gt => gt.TagId == tagId);
        }

        [Fact]
        public void AddPlatforms_ShouldAddPlatformIfNotPresent()
        {
            Game game = new Game { Id = Guid.NewGuid(), Titre = "Test Game" };
            Platform platform = new Platform { Id = Guid.NewGuid(), Nom = "PC" };

            game.AddPlatforms(new[] { platform });

            game.GamePlatforms.Should().ContainSingle(gp => gp.PlatformId == platform.Id);
        }

        [Fact]
        public void RemoveTag_ShouldRemoveTag()
        {
            Guid tagId = Guid.NewGuid();

            Game game = new Game { Id = Guid.NewGuid(), Titre = "Test Game" };
            Tag tag = new Tag { Id = tagId, Nom = "Action" };
            game.AddTags(new[] { tag });

            game.RemoveTag(tagId);

            game.GameTags.Should().BeEmpty();
        }

        [Fact]
        public void CreateNew_ShouldInitializeGameCorrectly()
        {
            // Arrange
            var game = new Game();
            var titre = "Hollow Knight";
            var description = "Metroidvania";
            var dateSortie = new DateTime(2017, 2, 24);
            var editeur = "Team Cherry";
            var imagePath = "hollowknight.jpg";

            game.CreateNew(titre, description, dateSortie, editeur, imagePath);

            game.Titre.Should().Be(titre);
            game.Description.Should().Be(description);
            game.DateSortie.Should().Be(dateSortie);
            game.Editeur.Should().Be(editeur);
            game.ImagePath.Should().Be(imagePath);
        }

        [Fact]
        public void RemovePlatform_ShouldRemovePlatform()
        {
            Game game = new Game();
            Guid platformId = Guid.NewGuid();
            Platform platform = new Platform { Id = platformId, Nom = "Switch" };
            game.AddPlatforms(new[] { platform });

            game.RemovePlatform(platformId);

            game.GamePlatforms.Should().BeEmpty();
        }

    }
}
