using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLogoutToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "UserBadges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "StudentCertifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Roadmaps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "ReviewerProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Problems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "ProblemAttempts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Modules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Certifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogout",
                table: "Badges",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "UserBadges");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "StudentCertifications");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "ProblemAttempts");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Certifications");

            migrationBuilder.DropColumn(
                name: "LastLogout",
                table: "Badges");
        }
    }
}
