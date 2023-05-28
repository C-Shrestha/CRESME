using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class mainmergetesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numColums",
                table: "Quiz",
                newName: "NumColumns");

            migrationBuilder.RenameColumn(
                name: "KeyWordsD",
                table: "Quiz",
                newName: "PhysicalE");

            migrationBuilder.RenameColumn(
                name: "KeyWordsC",
                table: "Quiz",
                newName: "ImagePos9");

            migrationBuilder.RenameColumn(
                name: "KeyWordsB",
                table: "Quiz",
                newName: "ImagePos8");

            migrationBuilder.RenameColumn(
                name: "KeyWordsA",
                table: "Quiz",
                newName: "ImagePos7");

            migrationBuilder.RenameColumn(
                name: "Image9Pos",
                table: "Quiz",
                newName: "ImagePos6");

            migrationBuilder.RenameColumn(
                name: "Image8Pos",
                table: "Quiz",
                newName: "ImagePos5");

            migrationBuilder.RenameColumn(
                name: "Image7Pos",
                table: "Quiz",
                newName: "ImagePos4");

            migrationBuilder.RenameColumn(
                name: "Image6Pos",
                table: "Quiz",
                newName: "ImagePos3");

            migrationBuilder.RenameColumn(
                name: "Image5Pos",
                table: "Quiz",
                newName: "ImagePos2");

            migrationBuilder.RenameColumn(
                name: "Image4Pos",
                table: "Quiz",
                newName: "ImagePos1");

            migrationBuilder.RenameColumn(
                name: "Image3Pos",
                table: "Quiz",
                newName: "ImagePos0");

            migrationBuilder.RenameColumn(
                name: "Image2Pos",
                table: "Quiz",
                newName: "Image0");

            migrationBuilder.RenameColumn(
                name: "Image1Pos",
                table: "Quiz",
                newName: "HistoryE");

            migrationBuilder.RenameColumn(
                name: "Image10Pos",
                table: "Quiz",
                newName: "FeedBackE");

            migrationBuilder.RenameColumn(
                name: "Image10",
                table: "Quiz",
                newName: "DiagnosticE");

            migrationBuilder.RenameColumn(
                name: "DiagnosisD",
                table: "Quiz",
                newName: "DiagnosisKeyWordsE");

            migrationBuilder.RenameColumn(
                name: "DiagnosisC",
                table: "Quiz",
                newName: "DiagnosisKeyWordsD");

            migrationBuilder.RenameColumn(
                name: "DiagnosisB",
                table: "Quiz",
                newName: "DiagnosisKeyWordsC");

            migrationBuilder.RenameColumn(
                name: "DiagnosisA",
                table: "Quiz",
                newName: "DiagnosisKeyWordsB");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisKeyWordsA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosisKeyWordsA",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "PhysicalE",
                table: "Quiz",
                newName: "KeyWordsD");

            migrationBuilder.RenameColumn(
                name: "NumColumns",
                table: "Quiz",
                newName: "numColums");

            migrationBuilder.RenameColumn(
                name: "ImagePos9",
                table: "Quiz",
                newName: "KeyWordsC");

            migrationBuilder.RenameColumn(
                name: "ImagePos8",
                table: "Quiz",
                newName: "KeyWordsB");

            migrationBuilder.RenameColumn(
                name: "ImagePos7",
                table: "Quiz",
                newName: "KeyWordsA");

            migrationBuilder.RenameColumn(
                name: "ImagePos6",
                table: "Quiz",
                newName: "Image9Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos5",
                table: "Quiz",
                newName: "Image8Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos4",
                table: "Quiz",
                newName: "Image7Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos3",
                table: "Quiz",
                newName: "Image6Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos2",
                table: "Quiz",
                newName: "Image5Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos1",
                table: "Quiz",
                newName: "Image4Pos");

            migrationBuilder.RenameColumn(
                name: "ImagePos0",
                table: "Quiz",
                newName: "Image3Pos");

            migrationBuilder.RenameColumn(
                name: "Image0",
                table: "Quiz",
                newName: "Image2Pos");

            migrationBuilder.RenameColumn(
                name: "HistoryE",
                table: "Quiz",
                newName: "Image1Pos");

            migrationBuilder.RenameColumn(
                name: "FeedBackE",
                table: "Quiz",
                newName: "Image10Pos");

            migrationBuilder.RenameColumn(
                name: "DiagnosticE",
                table: "Quiz",
                newName: "Image10");

            migrationBuilder.RenameColumn(
                name: "DiagnosisKeyWordsE",
                table: "Quiz",
                newName: "DiagnosisD");

            migrationBuilder.RenameColumn(
                name: "DiagnosisKeyWordsD",
                table: "Quiz",
                newName: "DiagnosisC");

            migrationBuilder.RenameColumn(
                name: "DiagnosisKeyWordsC",
                table: "Quiz",
                newName: "DiagnosisB");

            migrationBuilder.RenameColumn(
                name: "DiagnosisKeyWordsB",
                table: "Quiz",
                newName: "DiagnosisA");
        }
    }
}
