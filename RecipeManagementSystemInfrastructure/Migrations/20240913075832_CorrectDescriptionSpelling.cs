using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManagementSystemInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectDescriptionSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                table: "Recipes",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Recipes",
                newName: "Descripton");
        }
    }
}
