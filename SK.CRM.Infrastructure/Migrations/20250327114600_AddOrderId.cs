using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add the OrderId column
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty); // You can adjust default if needed

            // Create index for FK
            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            // Add FK constraint
            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Or Restrict, SetNull, etc.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            // Drop the index
            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails");

            // Drop the column
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderDetails");
        }
    }
}
