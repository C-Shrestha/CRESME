using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRESME.Migrations
{
    public partial class redoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizID = table.Column<int>(type: "int", nullable: true),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumColumns = table.Column<int>(type: "int", nullable: true),
                    PatientIntro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentNID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhysicalAnswerA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalAnswerE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticAnswerE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeResponseE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage0Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage1Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage2Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage3Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage4Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage5Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage6Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage7Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage8Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumImage9Clicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumLegendClicks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumLabValueClicks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.AttemptId);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumColumns = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Published = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedBackEnabled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIDAssignment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientIntro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Legend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HistoryE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisKeyWordsA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisKeyWordsB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisKeyWordsC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisKeyWordsD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisKeyWordsE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBackA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBackB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBackC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBackD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBackE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePos9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image0Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image5Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image6Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image7Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image8Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image9Alt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Course = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Course);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
