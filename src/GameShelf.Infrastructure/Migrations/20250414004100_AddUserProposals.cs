using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCreatedGames_Platforms_PlatformId",
                table: "UserCreatedGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCreatedGames_Users_UserId",
                table: "UserCreatedGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCreatedGames",
                table: "UserCreatedGames");

            migrationBuilder.RenameTable(
                name: "UserCreatedGames",
                newName: "UserProposals");

            migrationBuilder.RenameColumn(
                name: "NomPlateforme",
                table: "Platforms",
                newName: "Nom");

            migrationBuilder.RenameIndex(
                name: "IX_UserCreatedGames_UserId",
                table: "UserProposals",
                newName: "IX_UserProposals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCreatedGames_PlatformId",
                table: "UserProposals",
                newName: "IX_UserProposals_PlatformId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAjout",
                table: "UserGames",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Platforms",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProposals",
                table: "UserProposals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProposals_Platforms_PlatformId",
                table: "UserProposals",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProposals_Users_UserId",
                table: "UserProposals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProposals_Platforms_PlatformId",
                table: "UserProposals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProposals_Users_UserId",
                table: "UserProposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProposals",
                table: "UserProposals");

            migrationBuilder.DropColumn(
                name: "DateAjout",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Platforms");

            migrationBuilder.RenameTable(
                name: "UserProposals",
                newName: "UserCreatedGames");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Platforms",
                newName: "NomPlateforme");

            migrationBuilder.RenameIndex(
                name: "IX_UserProposals_UserId",
                table: "UserCreatedGames",
                newName: "IX_UserCreatedGames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProposals_PlatformId",
                table: "UserCreatedGames",
                newName: "IX_UserCreatedGames_PlatformId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCreatedGames",
                table: "UserCreatedGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreatedGames_Platforms_PlatformId",
                table: "UserCreatedGames",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCreatedGames_Users_UserId",
                table: "UserCreatedGames",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
