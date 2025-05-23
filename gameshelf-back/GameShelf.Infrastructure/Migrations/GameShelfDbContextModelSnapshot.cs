﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    [DbContext(typeof(GameShelfDbContext))]
    partial class GameShelfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameShelf.Domain.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateSortie")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Editeur")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uuid");

                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("GamePlatforms");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.GameTag", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("GameId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("GameTags");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.UserGame", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAjout")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePersoPath")
                        .HasColumnType("text");

                    b.Property<int?>("Note")
                        .HasColumnType("integer");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("UserGames");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.UserProposal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateSoumission")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uuid");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProposals");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.GamePlatform", b =>
                {
                    b.HasOne("GameShelf.Domain.Entities.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameShelf.Domain.Entities.Platform", "Platform")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.GameTag", b =>
                {
                    b.HasOne("GameShelf.Domain.Entities.Game", "Game")
                        .WithMany("GameTags")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameShelf.Domain.Entities.Tag", "Tag")
                        .WithMany("GameTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.UserGame", b =>
                {
                    b.HasOne("GameShelf.Domain.Entities.Game", "Game")
                        .WithMany("UserGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameShelf.Domain.Entities.User", "User")
                        .WithMany("UserGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.UserProposal", b =>
                {
                    b.HasOne("GameShelf.Domain.Entities.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameShelf.Domain.Entities.User", null)
                        .WithMany("Propositions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.Game", b =>
                {
                    b.Navigation("GamePlatforms");

                    b.Navigation("GameTags");

                    b.Navigation("UserGames");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.Platform", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.Tag", b =>
                {
                    b.Navigation("GameTags");
                });

            modelBuilder.Entity("GameShelf.Domain.Entities.User", b =>
                {
                    b.Navigation("Propositions");

                    b.Navigation("UserGames");
                });
#pragma warning restore 612, 618
        }
    }
}
