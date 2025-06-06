using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Inventory.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class adddiscountandtaxrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountRate",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxRate",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "Products");
        }
    }
}
