using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoadModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Roadmaps_RoadmapId",
                table: "Modules");

            migrationBuilder.AlterColumn<int>(
                name: "RoadmapId",
                table: "Modules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "RoadmapModules",
                columns: table => new
                {
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadmapModules", x => new { x.RoadmapId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_RoadmapModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadmapModules_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoadmapModules_ModuleId",
                table: "RoadmapModules",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Roadmaps_RoadmapId",
                table: "Modules",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Roadmaps_RoadmapId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "RoadmapModules");

            migrationBuilder.AlterColumn<int>(
                name: "RoadmapId",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Roadmaps_RoadmapId",
                table: "Modules",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
