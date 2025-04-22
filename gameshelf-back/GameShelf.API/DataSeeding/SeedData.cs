using GameShelf.Application.Interfaces;
using GameShelf.Domain.Entities;
using GameShelf.Domain.Enums;

namespace GameShelf.API.DataSeeding;

public static class SeedData
{
    public static async Task SeedDatabase(IGameShelfDbContext context)
    {
        // Seed Admin User
        if (!context.Users.Any(u => u.ExternalId == "dbe1a92d-c921-466d-957d-d1044e652f3f"))
        {
            User admin = new User();
            admin.Create(
                externalId: "dbe1a92d-c921-466d-957d-d1044e652f3f",
                email: "gameshelfadmin@yopmail.com",
                pseudo: "SuperAdmin",
                givenName: "Admin",
                surName: "User",
                role: UserRole.Admin
            );
            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }

        // Seed Genres
        if (!context.Tags.Any())
        {
            List<Tag> tags = new List<Tag>
                {
                    new Tag { Nom = "Action" },
                    new Tag { Nom = "Adventure" },
                    new Tag { Nom = "Puzzle" },
                    new Tag { Nom = "Strategy" },
                    new Tag { Nom = "Simulation" },
                    new Tag { Nom = "RPG" },
                    new Tag { Nom = "JRPG" },
                    new Tag { Nom = "ARPG" },
                    new Tag { Nom = "FPS" },
                    new Tag { Nom = "RTS" },
                    new Tag { Nom = "TBS" },
                    new Tag { Nom = "Gestion" },
                    new Tag { Nom = "Horreur" },
                    new Tag { Nom = "Plateforme 2D" },
                    new Tag { Nom = "Plateforme 3D" },
                    new Tag { Nom = "Escape" },
                    new Tag { Nom = "Narratif" },
                    new Tag { Nom = "Énigmes" },
                    new Tag { Nom = "Escape virtuel" },
                    new Tag { Nom = "Sport" },
                    new Tag { Nom = "Courses" },
                    new Tag { Nom = "Battle Royale" },
                    new Tag { Nom = "MMORPG" },
                    new Tag { Nom = "Coopération" },
                    new Tag { Nom = "Horreur psychologique" },
                    new Tag { Nom = "Survival" },
                    new Tag { Nom = "Survival horror" },
                    new Tag { Nom = "Sandbox" },
                    new Tag { Nom = "Open world" },
                    new Tag { Nom = "Rythme" },
                    new Tag { Nom = "Musique" },
                    new Tag { Nom = "Éducatif" }
                };
            await context.Tags.AddRangeAsync(tags);
            await context.SaveChangesAsync();
        }

        // Seed Platforms
        if (!context.Platforms.Any())
        {
            List<Platform> platforms = new List<Platform>
            {
                new Platform { Nom = "PC", ImagePath = "/images/platforms/pc.png"  },
                new Platform { Nom = "PS4", ImagePath = "/images/platforms/ps4.png"  },
                new Platform { Nom = "PS5", ImagePath = "/images/platforms/ps5.png"  },
                new Platform { Nom = "Xbox One", ImagePath = "/images/platforms/xboxone.png"  },
                new Platform { Nom = "Xbox Series X/S", ImagePath = "/images/platforms/xboxseries.png"  },
                new Platform { Nom = "Switch", ImagePath = "/images/platforms/switch.png"  },
            };
            await context.Platforms.AddRangeAsync(platforms);
            await context.SaveChangesAsync();
        }

        // Seed Games
        if (!context.Games.Any())
        {
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Titre = "The Last Of Us Part II",
                    Description = "The Last of Us Part II est un jeu vidéo d'action-aventure développé par Naughty Dog. Il se déroule dans un monde post-apocalyptique ravagé par une pandémie causée par le champignon Cordyceps. L'histoire suit Ellie, une jeune survivante, dans une quête de vengeance intense et émotionnelle, explorant des thèmes de moralité, de perte et de rédemption. Le jeu est connu pour son gameplay immersif, ses graphismes impressionnants et sa narration captivante.",
                    DateSortie = new DateTime(2020, 6, 19),
                    ImagePath = "/images/games/tlou2.webp",
                    Editeur = "Naughty Dog",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS5")?? throw new InvalidOperationException("Platform 'PS5' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Adventure") ?? throw new InvalidOperationException("Tag 'Adventure' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Narratif") ?? throw new InvalidOperationException("Tag 'Narratif' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Horreur psychologique") ?? throw new InvalidOperationException("Tag 'Horreur psychologique' not found.") }
                    }
                },
                new Game
                {
                    Titre = "God Of War Ragnarok",
                    Description = "God of War Ragnarök est un jeu vidéo d'action-aventure développé par Santa Monica Studio. Il suit Kratos et son fils Atreus dans une quête épique à travers les royaumes nordiques, alors qu'ils affrontent des dieux et des créatures mythologiques. Le jeu explore des thèmes de paternité, de sacrifice et de destin, tout en offrant un gameplay dynamique et des graphismes époustouflants.",
                    DateSortie = new DateTime(2022, 11, 9),
                    ImagePath = "/images/games/gowr.webp",
                    Editeur = "Santa Monica Studio",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS5")?? throw new InvalidOperationException("Platform 'PS5' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Adventure") ?? throw new InvalidOperationException("Tag 'Adventure' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Narratif") ?? throw new InvalidOperationException("Tag 'Narratif' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "RPG") ?? throw new InvalidOperationException("Tag 'RPG' not found.") }
                    }
                },
                new Game
                {
                    Titre = "Red Dead Redemption 2",
                    Description = "Red Dead Redemption 2 est un jeu vidéo d'action-aventure développé par Rockstar Games. Il se déroule dans un monde ouvert inspiré de l'Ouest américain à la fin du XIXe siècle. Le joueur incarne Arthur Morgan, un hors-la-loi, qui doit naviguer dans un monde en déclin tout en luttant pour sa survie et celle de sa bande. Le jeu est salué pour son histoire immersive, ses personnages profonds et son environnement richement détaillé.",
                    DateSortie = new DateTime(2018, 10, 26),
                    ImagePath = "/images/games/rdr2.webp",
                    Editeur = "Rockstar Games",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PC") ?? throw new InvalidOperationException("Platform 'PC' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Xbox One") ?? throw new InvalidOperationException("Platform 'Xbox One' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Adventure") ?? throw new InvalidOperationException("Tag 'Adventure' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Open world") ?? throw new InvalidOperationException("Tag 'Open world' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Narratif") ?? throw new InvalidOperationException("Tag 'Narratif' not found.") }
                    }
                },
                new Game
                {
                    Titre = "Minecraft",
                    Description = "Minecraft est un jeu vidéo de type bac à sable développé par Mojang Studios. Il plonge les joueurs dans un monde généré de manière procédurale, composé de blocs représentant divers matériaux. Les joueurs peuvent explorer, collecter des ressources, construire des structures et survivre face à des créatures hostiles. Avec ses modes Survie et Créatif, Minecraft offre une liberté totale pour créer, explorer et partager des aventures, seul ou en multijoueur.",
                    DateSortie = new DateTime(2011, 11, 18),
                    ImagePath = "/images/games/minecraft.webp",
                    Editeur = "Mojang Studios",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PC") ?? throw new InvalidOperationException("Platform 'PC' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Xbox One") ?? throw new InvalidOperationException("Platform 'Xbox One' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Switch") ?? throw new InvalidOperationException("Platform 'Switch' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Adventure") ?? throw new InvalidOperationException("Tag 'Adventure' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Open world") ?? throw new InvalidOperationException("Tag 'Open world' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Sandbox") ?? throw new InvalidOperationException("Tag 'Sandbox' not found.") }
                    }
                },
                new Game
                {
                    Titre = "Fortnite",
                    Description = "Fortnite est un jeu vidéo de bataille royale développé par Epic Games. Dans un monde post-apocalyptique, jusqu'à 100 joueurs s'affrontent pour être le dernier survivant. Le jeu se distingue par son style graphique coloré, sa mécanique de construction unique et ses événements en direct. Fortnite propose également un mode créatif où les joueurs peuvent concevoir leurs propres îles et jeux.",
                    DateSortie = new DateTime(2017, 7, 25),
                    ImagePath = "/images/games/fortnite.webp",
                    Editeur = "Epic Games",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PC") ?? throw new InvalidOperationException("Platform 'PC' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Xbox One") ?? throw new InvalidOperationException("Platform 'Xbox One' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Switch") ?? throw new InvalidOperationException("Platform 'Switch' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Battle Royale") ?? throw new InvalidOperationException("Tag 'Battle Royale' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Coopération") ?? throw new InvalidOperationException("Tag 'Coopération' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Survival") ?? throw new InvalidOperationException("Tag 'Survival' not found.") }
                    }
                },
                new Game
                {
                    Titre = "FC 25",
                    Description = "FC 25 est un jeu vidéo de simulation de football développé par EA Sports. Il propose une expérience immersive avec des graphismes réalistes, des mouvements fluides et une intelligence artificielle avancée. Les joueurs peuvent participer à des matchs en ligne, gérer des équipes et vivre la passion du football à travers divers modes de jeu.",
                    DateSortie = new DateTime(2023, 9, 29),
                    ImagePath = "/images/games/fc25.webp",
                    Editeur = "EA Sports",
                    GamePlatforms = new List<GamePlatform>
                    {
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PC") ?? throw new InvalidOperationException("Platform 'PC' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS4") ?? throw new InvalidOperationException("Platform 'PS4' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "PS5") ?? throw new InvalidOperationException("Platform 'PS5' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Xbox One") ?? throw new InvalidOperationException("Platform 'Xbox One' not found.") },
                        new GamePlatform { Platform = context.Platforms.FirstOrDefault(p => p.Nom == "Xbox Series X/S") ?? throw new InvalidOperationException("Platform 'Xbox Series X/S' not found.") }
                    },
                    GameTags = new List<GameTag>
                    {
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Action") ?? throw new InvalidOperationException("Tag 'Action' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Sport") ?? throw new InvalidOperationException("Tag 'Sport' not found.") },
                        new GameTag { Tag = context.Tags.FirstOrDefault(t => t.Nom == "Simulation") ?? throw new InvalidOperationException("Tag 'Simulation' not found.") }
                    }
                }
            };
            await context.Games.AddRangeAsync(games);
            await context.SaveChangesAsync();
        }
    }
}