using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFanWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentSeasonAndEpisodeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurentSeason",
                table: "UserAnime",
                newName: "CurrentSeason");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentSeason",
                table: "UserAnime",
                newName: "CurentSeason");
        }
    }
}
