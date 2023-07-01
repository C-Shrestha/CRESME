using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image9Pos",
                table: "Attempts",
                newName: "Image9Name");

            migrationBuilder.RenameColumn(
                name: "Image8Pos",
                table: "Attempts",
                newName: "Image8Name");

            migrationBuilder.RenameColumn(
                name: "Image7Pos",
                table: "Attempts",
                newName: "Image7Name");

            migrationBuilder.RenameColumn(
                name: "Image6Pos",
                table: "Attempts",
                newName: "Image6Name");

            migrationBuilder.RenameColumn(
                name: "Image5Pos",
                table: "Attempts",
                newName: "Image5Name");

            migrationBuilder.RenameColumn(
                name: "Image4Pos",
                table: "Attempts",
                newName: "Image4Name");

            migrationBuilder.RenameColumn(
                name: "Image3Pos",
                table: "Attempts",
                newName: "Image3Name");

            migrationBuilder.RenameColumn(
                name: "Image2Pos",
                table: "Attempts",
                newName: "Image2Name");

            migrationBuilder.RenameColumn(
                name: "Image1Pos",
                table: "Attempts",
                newName: "Image1Name");

            migrationBuilder.RenameColumn(
                name: "Image0Pos",
                table: "Attempts",
                newName: "Image0Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image9Name",
                table: "Attempts",
                newName: "Image9Pos");

            migrationBuilder.RenameColumn(
                name: "Image8Name",
                table: "Attempts",
                newName: "Image8Pos");

            migrationBuilder.RenameColumn(
                name: "Image7Name",
                table: "Attempts",
                newName: "Image7Pos");

            migrationBuilder.RenameColumn(
                name: "Image6Name",
                table: "Attempts",
                newName: "Image6Pos");

            migrationBuilder.RenameColumn(
                name: "Image5Name",
                table: "Attempts",
                newName: "Image5Pos");

            migrationBuilder.RenameColumn(
                name: "Image4Name",
                table: "Attempts",
                newName: "Image4Pos");

            migrationBuilder.RenameColumn(
                name: "Image3Name",
                table: "Attempts",
                newName: "Image3Pos");

            migrationBuilder.RenameColumn(
                name: "Image2Name",
                table: "Attempts",
                newName: "Image2Pos");

            migrationBuilder.RenameColumn(
                name: "Image1Name",
                table: "Attempts",
                newName: "Image1Pos");

            migrationBuilder.RenameColumn(
                name: "Image0Name",
                table: "Attempts",
                newName: "Image0Pos");
        }
    }
}
