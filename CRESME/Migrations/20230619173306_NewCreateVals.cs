using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class NewCreateVals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image0Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image9Alt",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Legend",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image0Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image1Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image2Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image3Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image4Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image5Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image6Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image7Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image8Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image9Alt",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Legend",
                table: "Quiz");
        }
    }
}
