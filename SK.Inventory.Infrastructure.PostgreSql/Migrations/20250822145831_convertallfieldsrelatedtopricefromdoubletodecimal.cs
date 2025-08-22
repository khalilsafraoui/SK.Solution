using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Inventory.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class convertallfieldsrelatedtopricefromdoubletodecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountRate",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TaxRate",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "DiscountRate",
                table: "Products",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);
        }
    }
}
