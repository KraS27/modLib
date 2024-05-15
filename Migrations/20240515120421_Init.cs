using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace modLib.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "modPacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modPacks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    path = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mod_modPack",
                columns: table => new
                {
                    ModId = table.Column<int>(type: "int", nullable: false),
                    ModPackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mod_modPack", x => new { x.ModId, x.ModPackId });
                    table.ForeignKey(
                        name: "FK_mod_modPack_modPacks_ModPackId",
                        column: x => x.ModPackId,
                        principalTable: "modPacks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mod_modPack_mods_ModId",
                        column: x => x.ModId,
                        principalTable: "mods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mod_modPack_ModPackId",
                table: "mod_modPack",
                column: "ModPackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mod_modPack");

            migrationBuilder.DropTable(
                name: "modPacks");

            migrationBuilder.DropTable(
                name: "mods");
        }
    }
}
