﻿// <auto-generated />
using System;
using CRESME.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRESME.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230615042447_startagainmigration")]
    partial class startagainmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CRESME.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Block")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Term")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CRESME.Data.Attempt", b =>
                {
                    b.Property<int>("AttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttemptId"), 1L, 1);

                    b.Property<string>("Block")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticAnswerA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticAnswerB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticAnswerC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticAnswerD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticAnswerE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FreeResponseA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreeResponseB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreeResponseC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreeResponseD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FreeResponseE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage0Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage1Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage2Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage3Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage4Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage5Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage6Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage7Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage8Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumImage9Clicks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientIntro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalAnswerA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalAnswerB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalAnswerC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalAnswerD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalAnswerE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuizID")
                        .HasColumnType("int");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Term")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AttemptId");

                    b.ToTable("Attempts");
                });

            modelBuilder.Entity("CRESME.Data.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizId"), 1L, 1);

                    b.Property<string>("Block")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DiagnosisKeyWordsA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosisKeyWordsB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosisKeyWordsC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosisKeyWordsD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosisKeyWordsE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiagnosticE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeedBackA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedBackB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedBackC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedBackD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedBackE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeedBackEnabled")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoryE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image0")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos0")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePos9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIDAssignment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumColumns")
                        .HasColumnType("int");

                    b.Property<string>("PatientIntro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhysicalE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Published")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuizName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Term")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuizId");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("CRESME.Data.Test", b =>
                {
                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Course");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CRESME.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CRESME.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRESME.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CRESME.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
