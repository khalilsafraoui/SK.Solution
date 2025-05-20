using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Visit.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsRelatedToTripInDestinationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Destinations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Destinations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkipReason",
                table: "Destinations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Destinations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "SkipReason",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Destinations");
        }
    }
}
