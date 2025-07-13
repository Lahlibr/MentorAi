using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MentorAi_backd.Migrations
{
    /// <inheritdoc />
    public partial class studentReviweren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserRole",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CertificateUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewerProfiles",
                columns: table => new
                {
                    UserId1 = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpertiseAreasJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: false),
                    ReviewsCompleted = table.Column<int>(type: "int", nullable: false),
                    IsAvailableForReviews = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerProfiles", x => x.UserId1);
                    table.ForeignKey(
                        name: "FK_ReviewerProfiles_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roadmaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedCompletionHours = table.Column<int>(type: "int", nullable: false),
                    TotalModules = table.Column<int>(type: "int", nullable: false),
                    TotalChallenges = table.Column<int>(type: "int", nullable: false),
                    TotalCertifications = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roadmaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    UserId1 = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AssessmentScore = table.Column<int>(type: "int", nullable: true),
                    CurrentLearningGoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredLearningStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.UserId1);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderInRoadmap = table.Column<int>(type: "int", nullable: false),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCertifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false),
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCertifications_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCertifications_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId1",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBadges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false),
                    BadgeId = table.Column<int>(type: "int", nullable: false),
                    AwardedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBadges_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId1",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    InputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExampleTestCasesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolutionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedSolutionHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderInModule = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problems_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentRoadmapProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    CurrentProgressPercentage = table.Column<double>(type: "float", nullable: false),
                    CurrentModuleId = table.Column<int>(type: "int", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRoadmapProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRoadmapProgresses_Modules_CurrentModuleId",
                        column: x => x.CurrentModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StudentRoadmapProgresses_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentRoadmapProgresses_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId1",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProblemAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    SubmittedCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    ExecutionTimeMs = table.Column<double>(type: "float", nullable: false),
                    MemoryUsageBytes = table.Column<long>(type: "bigint", nullable: false),
                    CompilerOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResultsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemAttempts_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProblemAttempts_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "UserId1",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    ProblemAttemptId = table.Column<int>(type: "int", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualReviewStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualReviewEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    RatingGivenByStudent = table.Column<double>(type: "float", nullable: true),
                    VideoConferenceLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ProblemAttempts_ProblemAttemptId",
                        column: x => x.ProblemAttemptId,
                        principalTable: "ProblemAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "CreatedAt", "Criteria", "DeletedAt", "Description", "ImageUrl", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "Name" },
                values: new object[] { 1, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(8041), "Submit any problem solution.", null, "Awarded for submitting your first coding solution.", "https://placehold.co/50x50/28a745/ffffff?text=Badge1", false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(8041), null, "First Code" });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "Id", "CertificateUrl", "CreatedAt", "Criteria", "DeletedAt", "Description", "ImageUrl", "IsDeleted", "IssuedDate", "Issuer", "LastUpdatedAt", "LastUpdatedBy", "Title" },
                values: new object[] { 1, "https://mentorai.com/cert/csharp-1", new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7996), "Complete 'Beginner C# Development' roadmap.", null, "Demonstrates basic proficiency in C#.", "https://placehold.co/100x100/007bff/ffffff?text=C#Cert", false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7995), "MentorAI", new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7996), null, "C# Fundamentals Certified" });

            migrationBuilder.InsertData(
                table: "Roadmaps",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "DifficultyLevel", "EstimatedCompletionHours", "ImageUrl", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "Title", "TotalCertifications", "TotalChallenges", "TotalModules" },
                values: new object[] { 1, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7866), null, "Learn the fundamentals of C# programming.", "Beginner", 40, null, false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7867), null, "Beginner C# Development", 0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AcceptedTermsAt", "CreatedAt", "DeletedAt", "Email", "EmailVerified", "FailedLoginAttempts", "IsDeleted", "LastFailedLogin", "LastSuccessfulLogin", "LastUpdatedAt", "LastUpdatedBy", "LockoutEnd", "OtpSecret", "Password", "PasswordResetToken", "PasswordResetTokenExpiry", "PhoneNumber", "ProfileCompleted", "ProfileImageUrl", "RefreshToken", "RefreshTokenExpiryTime", "Status", "TwoFactorEnabled", "UserName", "UserRole", "VerificationToken", "VerificationTokenExpiry" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 7, 12, 15, 39, 50, 891, DateTimeKind.Utc).AddTicks(9728), null, "alice@example.com", true, 0, false, null, null, new DateTime(2025, 7, 12, 15, 39, 50, 891, DateTimeKind.Utc).AddTicks(9736), null, null, null, "$2a$11$FwEQIIFNE39ZUc/zXmrWFOTlpdJBisAOlqQiYVkEVfhUOip51Au2.", null, null, null, true, null, null, null, "Active", false, "alice_student", "Student", null, null },
                    { 2, null, new DateTime(2025, 7, 12, 15, 39, 51, 11, DateTimeKind.Utc).AddTicks(4526), null, "bob@example.com", true, 0, false, null, null, new DateTime(2025, 7, 12, 15, 39, 51, 11, DateTimeKind.Utc).AddTicks(4530), null, null, null, "$2a$11$CwuiXus48ct36t3NSJGZ2.CqSBRRu2roSi/p8dnbtgDClvm6PEtDS", null, null, null, true, null, null, null, "Active", false, "bob_reviewer", "Reviewer", null, null },
                    { 3, null, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(6707), null, "charlie@example.com", true, 0, false, null, null, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(6715), null, null, null, "$2a$11$1A5KnmG7VNlq0AipmgmyRuowxtnubAgGr0gF4FTqoTokbtfaEDbPm", null, null, null, true, null, null, null, "Active", false, "charlie_admin", "Admin", null, null }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "OrderInRoadmap", "RoadmapId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7916), null, "Basic syntax and variables.", false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7916), null, 1, 1, "Introduction to C#" },
                    { 2, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7919), null, "If statements and loops.", false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7919), null, 2, 1, "Control Flow" }
                });

            migrationBuilder.InsertData(
                table: "ReviewerProfiles",
                columns: new[] { "UserId1", "Availability", "AverageRating", "Bio", "CreatedAt", "DeletedAt", "ExpertiseAreasJson", "UserId", "IsAvailableForReviews", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "ReviewsCompleted", "YearsOfExperience" },
                values: new object[] { 2, "Weekdays", 4.5, "Experienced .NET developer with a passion for clean code and mentorship.", new DateTime(2025, 7, 12, 15, 39, 50, 775, DateTimeKind.Utc).AddTicks(5968), null, "[\"C#\", \".NET Core\", \"ASP.NET\", \"SQL Server\", \"Azure\"]", 2, true, false, new DateTime(2025, 7, 12, 15, 39, 50, 775, DateTimeKind.Utc).AddTicks(5969), null, 50, 5 });

            migrationBuilder.InsertData(
                table: "StudentProfiles",
                columns: new[] { "UserId1", "Age", "AssessmentScore", "CreatedAt", "CurrentLearningGoal", "DeletedAt", "UserId", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "PreferredLearningStyle" },
                values: new object[] { 1, 22, 80, new DateTime(2025, 7, 12, 15, 39, 50, 777, DateTimeKind.Utc).AddTicks(305), "Master Algorithms", null, 1, false, new DateTime(2025, 7, 12, 15, 39, 50, 777, DateTimeKind.Utc).AddTicks(306), null, "Visual" });

            migrationBuilder.InsertData(
                table: "Problems",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "DifficultyLevel", "ExampleTestCasesJson", "ExpectedSolutionHash", "ImageUrl", "InputFormat", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "ModuleId", "OrderInModule", "OutputFormat", "SolutionTemplate", "Title" },
                values: new object[] { 1, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7966), null, "Write a function that takes two integers and returns their sum.", 0, "[{\"input\":\"1,2\",\"output\":\"3\"},{\"input\":\"-5,10\",\"output\":\"5\"}]", "dummyhash1", null, "Two integers separated by a comma.", false, new DateTime(2025, 7, 12, 15, 39, 51, 134, DateTimeKind.Utc).AddTicks(7966), null, 1, 1, "Their sum.", "public class Solution { public int Sum(int a, int b) { /* Your code here */ return 0; } }", "Sum of Two Numbers" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_RoadmapId",
                table: "Modules",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemAttempts_ProblemId",
                table: "ProblemAttempts",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemAttempts_StudentProfileId",
                table: "ProblemAttempts",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_ModuleId",
                table: "Problems",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProblemAttemptId",
                table: "Reviews",
                column: "ProblemAttemptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCertifications_CertificationId",
                table: "StudentCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCertifications_StudentProfileId_CertificationId",
                table: "StudentCertifications",
                columns: new[] { "StudentProfileId", "CertificationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentRoadmapProgresses_CurrentModuleId",
                table: "StudentRoadmapProgresses",
                column: "CurrentModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRoadmapProgresses_RoadmapId",
                table: "StudentRoadmapProgresses",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRoadmapProgresses_StudentProfileId",
                table: "StudentRoadmapProgresses",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_BadgeId",
                table: "UserBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_StudentProfileId_BadgeId",
                table: "UserBadges",
                columns: new[] { "StudentProfileId", "BadgeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewerProfiles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StudentCertifications");

            migrationBuilder.DropTable(
                name: "StudentRoadmapProgresses");

            migrationBuilder.DropTable(
                name: "UserBadges");

            migrationBuilder.DropTable(
                name: "ProblemAttempts");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Roadmaps");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
