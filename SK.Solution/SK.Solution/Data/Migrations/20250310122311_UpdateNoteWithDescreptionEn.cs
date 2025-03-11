using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Solution.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoteWithDescreptionEn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Notes");
        }
    }
}
