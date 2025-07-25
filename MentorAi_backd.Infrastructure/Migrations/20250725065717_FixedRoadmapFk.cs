using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedRoadmapFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Roadmaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoadmapId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_RoadmapId",
                table: "Problems",
                column: "RoadmapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Roadmaps_RoadmapId",
                table: "Problems",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Roadmaps_RoadmapId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_RoadmapId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Roadmaps");

            migrationBuilder.DropColumn(
                name: "RoadmapId",
                table: "Problems");
        }
    }
}
