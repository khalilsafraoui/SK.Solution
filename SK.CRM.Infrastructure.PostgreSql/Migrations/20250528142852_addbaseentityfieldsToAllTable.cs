using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.CRM.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class addbaseentityfieldsToAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "ShoppingCarts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ShoppingCarts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "ShoppingCarts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ShoppingCarts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "OrderDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "OrderDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Customers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Customers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderDetails_Quantity_Positive",
                table: "OrderDetails",
                sql: "\"Quantity\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderDetails_Quantity_Positive",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Addresses");
        }
    }
}
