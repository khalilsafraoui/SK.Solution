using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Visit.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class CreateVisitDBAndDestinationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AddressId = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsDelevery = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityId = table.Column<string>(type: "text", nullable: false),
                    StateId = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destinations");
        }
    }
}
