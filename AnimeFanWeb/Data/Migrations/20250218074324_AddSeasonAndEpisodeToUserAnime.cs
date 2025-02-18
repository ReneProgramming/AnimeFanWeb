using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFanWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonAndEpisodeToUserAnime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurentSeason",
                table: "UserAnime",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentEpisode",
                table: "UserAnime",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurentSeason",
                table: "UserAnime");

            migrationBuilder.DropColumn(
                name: "CurrentEpisode",
                table: "UserAnime");
        }
    }
}
