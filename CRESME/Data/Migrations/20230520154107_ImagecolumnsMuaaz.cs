using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class ImagecolumnsMuaaz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPublished",
                table: "Quiz",
                newName: "Published");

            migrationBuilder.AddColumn<string>(
                name: "Image10Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image9Pos",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumImages",
                table: "Quiz",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientInto",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image10Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image1Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image2Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image3Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image4Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image5Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image6Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image7Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image8Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image9Pos",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "NumImages",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "PatientInto",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "Published",
                table: "Quiz",
                newName: "isPublished");
        }
    }
}
