using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Renamedarraypropertiestopluralvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailUser_Email_EmailId",
                table: "EmailUser");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "EmailUser",
                newName: "EmailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailUser_Email_EmailsId",
                table: "EmailUser",
                column: "EmailsId",
                principalTable: "Email",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailUser_Email_EmailsId",
                table: "EmailUser");

            migrationBuilder.RenameColumn(
                name: "EmailsId",
                table: "EmailUser",
                newName: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailUser_Email_EmailId",
                table: "EmailUser",
                column: "EmailId",
                principalTable: "Email",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
