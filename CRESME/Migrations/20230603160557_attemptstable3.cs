using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class attemptstable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diagnosticAnswerB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage0Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage1Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage2Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage3Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage4Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage5Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage6Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage7Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage8Clicks = table.Column<int>(type: "int", nullable: true),
                    NumImage9Clicks = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.AttemptId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attempts");
        }
    }
}
