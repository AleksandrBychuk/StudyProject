using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedExtraFieldsFromRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionId",
                table: "PermissionRole");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "PermissionRole",
                newName: "PermissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionsId",
                table: "PermissionRole",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionsId",
                table: "PermissionRole");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "PermissionRole",
                newName: "PermissionId");

            migrationBuilder.AddColumn<Guid>(
                name: "PermissionId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionId",
                table: "PermissionRole",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
