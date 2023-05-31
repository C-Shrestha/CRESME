using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class createquizapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TermAssignment",
                table: "Quiz",
                newName: "Term");

            migrationBuilder.RenameColumn(
                name: "CourseAssignment",
                table: "Quiz",
                newName: "Course");

            migrationBuilder.RenameColumn(
                name: "BlockAssignment",
                table: "Quiz",
                newName: "Block");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Term",
                table: "Quiz",
                newName: "TermAssignment");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Quiz",
                newName: "CourseAssignment");

            migrationBuilder.RenameColumn(
                name: "Block",
                table: "Quiz",
                newName: "BlockAssignment");
        }
    }
}
