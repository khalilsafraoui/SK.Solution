using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.CRM.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class convertallfieldsrelatedtopricefromdoubletodecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "Quotes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "GlobalDiscountRate",
                table: "Quotes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "QuoteItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "QuoteItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountRate",
                table: "QuoteItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                table: "Orders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "OrderDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountRate",
                table: "OrderDetails",
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
                table: "Quotes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "GlobalDiscountRate",
                table: "Quotes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "QuoteItems",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "TaxRate",
                table: "QuoteItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "DiscountRate",
                table: "QuoteItems",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "OrderTotal",
                table: "Orders",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "TaxRate",
                table: "OrderDetails",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "OrderDetails",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<double>(
                name: "DiscountRate",
                table: "OrderDetails",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);
        }
    }
}
