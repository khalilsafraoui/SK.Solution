using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Visit.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsRelatedToTripInDestinationEntityV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalTime",
                table: "Destinations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishTime",
                table: "Destinations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Destinations");
        }
    }
}
