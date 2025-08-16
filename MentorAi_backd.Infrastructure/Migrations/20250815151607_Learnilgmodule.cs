using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Learnilgmodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "Roadmaps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isLocked",
                table: "Roadmaps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "originalPrice",
                table: "Roadmaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Roadmaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "projects",
                table: "Roadmaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Certificate",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedHours",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LearningOutcomes",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "Projects",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Topics",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

           

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "isLocked",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "originalPrice",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "projects",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "Certificate",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "EstimatedHours",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "LearningOutcomes",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Projects",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Topics",
                table: "Modules");

           

            
        }
    }
}
