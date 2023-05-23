using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class merge2Muaaz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image10",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "Image9",
                table: "Quiz",
                newName: "imageFile7");

            migrationBuilder.RenameColumn(
                name: "Image8",
                table: "Quiz",
                newName: "imageFile6");

            migrationBuilder.RenameColumn(
                name: "Image7",
                table: "Quiz",
                newName: "imageFile5");

            migrationBuilder.RenameColumn(
                name: "Image6",
                table: "Quiz",
                newName: "imageFile4");

            migrationBuilder.RenameColumn(
                name: "Image5",
                table: "Quiz",
                newName: "imageFile3");

            migrationBuilder.RenameColumn(
                name: "Image4",
                table: "Quiz",
                newName: "imageFile2");

            migrationBuilder.RenameColumn(
                name: "Image3",
                table: "Quiz",
                newName: "imageFile1");

            migrationBuilder.RenameColumn(
                name: "Image2",
                table: "Quiz",
                newName: "imageFile0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageFile7",
                table: "Quiz",
                newName: "Image9");

            migrationBuilder.RenameColumn(
                name: "imageFile6",
                table: "Quiz",
                newName: "Image8");

            migrationBuilder.RenameColumn(
                name: "imageFile5",
                table: "Quiz",
                newName: "Image7");

            migrationBuilder.RenameColumn(
                name: "imageFile4",
                table: "Quiz",
                newName: "Image6");

            migrationBuilder.RenameColumn(
                name: "imageFile3",
                table: "Quiz",
                newName: "Image5");

            migrationBuilder.RenameColumn(
                name: "imageFile2",
                table: "Quiz",
                newName: "Image4");

            migrationBuilder.RenameColumn(
                name: "imageFile1",
                table: "Quiz",
                newName: "Image3");

            migrationBuilder.RenameColumn(
                name: "imageFile0",
                table: "Quiz",
                newName: "Image2");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image10",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
