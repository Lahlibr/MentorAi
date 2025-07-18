using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateissues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GratuationYear",
                table: "StudentProfiles",
                newName: "GraduationYear");

            migrationBuilder.RenameColumn(
                name: "Gratuation",
                table: "StudentProfiles",
                newName: "Graduation");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "StudentProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GraduationYear",
                table: "StudentProfiles",
                newName: "GratuationYear");

            migrationBuilder.RenameColumn(
                name: "Graduation",
                table: "StudentProfiles",
                newName: "Gratuation");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "StudentProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
