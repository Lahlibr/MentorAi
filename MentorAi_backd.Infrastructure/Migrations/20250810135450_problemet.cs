using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentorAi_backd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class problemet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExampleInputsJson",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExampleOutputsJson",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HiddenTestCasesJson",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ExampleInputsJson",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ExampleOutputsJson",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "HiddenTestCasesJson",
                table: "Problems");

            
        }
    }
}
