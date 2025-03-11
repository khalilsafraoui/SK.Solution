using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SK.Solution.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdForNoteAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "NoteCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NoteCategories");
        }
    }
}
