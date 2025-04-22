﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShelf.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserUnicity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ExternalId",
                table: "Users");
        }
    }
}
