using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class flatteningattempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "NumColumns",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "QuizName",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "StudentNID",
                table: "Attempts");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Attempts");

            migrationBuilder.RenameColumn(
                name: "Term",
                table: "Attempts",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "QuizID",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Attempts",
                newName: "Term");

            migrationBuilder.AlterColumn<int>(
                name: "QuizID",
                table: "Attempts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddColumn<int>(
                name: "NumColumns",
                table: "Attempts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QuizName",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNID",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
