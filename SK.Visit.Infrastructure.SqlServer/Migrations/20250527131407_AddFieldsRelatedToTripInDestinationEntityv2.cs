using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Visit.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsRelatedToTripInDestinationEntityv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalTime",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishTime",
                table: "Destinations",
                type: "nvarchar(max)",
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
