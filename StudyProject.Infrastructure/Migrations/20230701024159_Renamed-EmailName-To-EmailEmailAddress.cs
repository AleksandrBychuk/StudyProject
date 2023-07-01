using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedEmailNameToEmailEmailAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Email",
                newName: "EmailAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Email",
                newName: "Name");
        }
    }
}
