using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class profileupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReviewerProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "GuardianEmail",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "ReviewerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReviewerAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerProfileId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewerAvailabilities_ReviewerProfiles_ReviewerProfileId",
                        column: x => x.ReviewerProfileId,
                        principalTable: "ReviewerProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerAvailabilities_ReviewerProfileId",
                table: "ReviewerAvailabilities",
                column: "ReviewerProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewerAvailabilities");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "ReviewerProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "GuardianEmail",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "ReviewerProfiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReviewerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
