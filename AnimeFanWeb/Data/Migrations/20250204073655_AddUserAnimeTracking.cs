using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFanWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAnimeTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAnime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnimeListId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnime_AnimeList_AnimeListId",
                        column: x => x.AnimeListId,
                        principalTable: "AnimeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnime_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnime_AnimeListId",
                table: "UserAnime",
                column: "AnimeListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnime_UserId",
                table: "UserAnime",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnime");
        }
    }
}
