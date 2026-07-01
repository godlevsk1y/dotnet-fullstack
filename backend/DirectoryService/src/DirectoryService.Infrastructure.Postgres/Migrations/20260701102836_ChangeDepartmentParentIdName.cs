using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDepartmentParentIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "departments",
                newName: "parent_id");

            migrationBuilder.RenameIndex(
                name: "IX_departments_ParentId",
                table: "departments",
                newName: "IX_departments_parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "departments",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_parent_id",
                table: "departments",
                newName: "IX_departments_ParentId");
        }
    }
}
