using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId1",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
        name: "OrderId",
        table: "OrderDetails",
        type: "int",
        nullable: true); // or false depending on your model

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId1",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId1",
                table: "OrderDetails",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId1",
                table: "OrderDetails",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
