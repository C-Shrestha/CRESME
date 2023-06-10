using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class mergescoringdrag2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuizName",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "QuizID",
                table: "Attempts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizID",
                table: "Attempts");

            migrationBuilder.AlterColumn<string>(
                name: "QuizName",
                table: "Attempts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
