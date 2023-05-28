using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Data.Migrations
{
    public partial class muaaz2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockAssignment",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "CourseAssignment",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosisA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosisB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosisC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosisD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosticA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosticB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosticC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "DiagnosticD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "FeedBackA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "FeedBackB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "FeedBackC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "FeedBackD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "FeedBackEnabled",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HistoryA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HistoryB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HistoryC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "HistoryD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image10",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image6",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image7",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image8",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Image9",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "KeyWordsA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "KeyWordsB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "KeyWordsC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "KeyWordsD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "PhysicalA",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "PhysicalB",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "PhysicalC",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "PhysicalD",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "StudentAssignment",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Test");

            migrationBuilder.RenameColumn(
                name: "QuizName",
                table: "Test",
                newName: "Course");

            migrationBuilder.RenameColumn(
                name: "NID",
                table: "Quiz",
                newName: "YearAssignment");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Quiz",
                newName: "QuizName");

            migrationBuilder.AddColumn<string>(
                name: "NID",
                table: "Test",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlockAssignment",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseAssignment",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FeedBackA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedBackB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedBackC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedBackD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FeedBackEnabled",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HistoryA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image6",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image7",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image8",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image9",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIDAssignment",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalA",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalB",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalC",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalD",
                table: "Quiz",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NID",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "BlockAssignment",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "CourseAssignment",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosisA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosisB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosisC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosisD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosticA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosticB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosticC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "DiagnosticD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "FeedBackA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "FeedBackB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "FeedBackC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "FeedBackD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "FeedBackEnabled",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "HistoryA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "HistoryB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "HistoryC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "HistoryD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image10",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image5",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image6",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image7",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image8",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Image9",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "KeyWordsA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "KeyWordsB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "KeyWordsC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "KeyWordsD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "NIDAssignment",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "PhysicalA",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "PhysicalB",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "PhysicalC",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "PhysicalD",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Test",
                newName: "QuizName");

            migrationBuilder.RenameColumn(
                name: "YearAssignment",
                table: "Quiz",
                newName: "NID");

            migrationBuilder.RenameColumn(
                name: "QuizName",
                table: "Quiz",
                newName: "Course");

            migrationBuilder.AddColumn<string>(
                name: "BlockAssignment",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CourseAssignment",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FeedBackA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedBackB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedBackC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedBackD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "FeedBackEnabled",
                table: "Test",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HistoryA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HistoryB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HistoryC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HistoryD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image10",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image5",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image6",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image7",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image8",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image9",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyWordsD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalA",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalB",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalC",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalD",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Test",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StudentAssignment",
                table: "Test",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Test",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
