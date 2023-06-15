using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class newattemptcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientIntro",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "PatientIntro",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "Attempts");
        }
    }
}
