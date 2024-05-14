using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace modLib.Migrations
{
    /// <inheritdoc />
    public partial class AddmodPackmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ModPackId",
                table: "Mods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ModPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModPacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mods_ModPackId",
                table: "Mods",
                column: "ModPackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mods_ModPacks_ModPackId",
                table: "Mods",
                column: "ModPackId",
                principalTable: "ModPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mods_ModPacks_ModPackId",
                table: "Mods");

            migrationBuilder.DropTable(
                name: "ModPacks");

            migrationBuilder.DropIndex(
                name: "IX_Mods_ModPackId",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "ModPackId",
                table: "Mods");
        }
    }
}
