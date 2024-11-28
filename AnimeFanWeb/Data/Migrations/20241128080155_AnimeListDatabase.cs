using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFanWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnimeListDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AnimeList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AnimeList");
        }
    }
}
