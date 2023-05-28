using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class Muaaz3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearAssignment",
                table: "Quiz");

            migrationBuilder.AlterColumn<string>(
                name: "isPublished",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FeedBackEnabled",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isPublished",
                table: "Quiz",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "FeedBackEnabled",
                table: "Quiz",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "YearAssignment",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
