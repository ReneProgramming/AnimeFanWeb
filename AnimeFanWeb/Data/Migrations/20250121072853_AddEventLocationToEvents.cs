using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFanWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventLocationToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventLocation",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventLocation",
                table: "Events");
        }
    }
}
