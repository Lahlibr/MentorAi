using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserBadges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StudentRoadmapProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentRoadmapProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StudentProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StudentCertifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Roadmaps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ReviewerProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Problems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProblemAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Certifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Badges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StudentRoadmapProgresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentRoadmapProgresses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StudentCertifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProblemAttempts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Certifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Badges");
        }
    }
}
