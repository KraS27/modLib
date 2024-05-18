using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace modLib.Migrations
{
    /// <inheritdoc />
    public partial class AddGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "mods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "modPacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    version = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mods_GameId",
                table: "mods",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_modPacks_GameId",
                table: "modPacks",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_modPacks_games_GameId",
                table: "modPacks",
                column: "GameId",
                principalTable: "games",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mods_games_GameId",
                table: "mods",
                column: "GameId",
                principalTable: "games",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modPacks_games_GameId",
                table: "modPacks");

            migrationBuilder.DropForeignKey(
                name: "FK_mods_games_GameId",
                table: "mods");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropIndex(
                name: "IX_mods_GameId",
                table: "mods");

            migrationBuilder.DropIndex(
                name: "IX_modPacks_GameId",
                table: "modPacks");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "mods");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "modPacks");
        }
    }
}
