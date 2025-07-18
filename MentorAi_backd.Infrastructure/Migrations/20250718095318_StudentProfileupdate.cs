using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentProfileupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gratuation",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GratuationYear",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GuardianEmail",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianName",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianPhoneNumber",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuardianRelationship",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gratuation",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GratuationYear",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GuardianEmail",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GuardianName",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GuardianPhoneNumber",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GuardianRelationship",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "University",
                table: "StudentProfiles");
        }
    }
}
