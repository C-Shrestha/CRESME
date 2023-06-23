using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class instructormigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructorID",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorID",
                table: "Quiz");
        }
    }
}
