using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class introMuaaz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientInto",
                table: "Quiz",
                newName: "PatientIntro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientIntro",
                table: "Quiz",
                newName: "PatientInto");
        }
    }
}
