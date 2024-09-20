using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hrs.Infrastructure.Database.Migrations.Admin
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0732be6e-3b7b-4046-83f0-781fb2586e7f"));

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("305d0f18-7b06-4ff4-963a-e5480f67d4c8"));

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd49d739-e238-42a3-a55d-22c1ae23092c"));

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "admin",
                table: "Roles");

            migrationBuilder.InsertData(
                schema: "admin",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "LastModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0c6fe1d1-fc42-4a2f-bfe6-2dddcc8e8c7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Receptionist" },
                    { new Guid("e1ecba8a-84ca-436c-9a73-fc62dfa7f5c8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin" },
                    { new Guid("f2e768c6-1336-4712-a0c8-b53cc38782aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0c6fe1d1-fc42-4a2f-bfe6-2dddcc8e8c7c"));

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e1ecba8a-84ca-436c-9a73-fc62dfa7f5c8"));

            migrationBuilder.DeleteData(
                schema: "admin",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2e768c6-1336-4712-a0c8-b53cc38782aa"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "admin",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "admin",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "LastModifiedAt", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("0732be6e-3b7b-4046-83f0-781fb2586e7f"), new DateTime(2024, 8, 31, 19, 39, 14, 799, DateTimeKind.Local).AddTicks(8711), null, "Manager", null },
                    { new Guid("305d0f18-7b06-4ff4-963a-e5480f67d4c8"), new DateTime(2024, 8, 31, 19, 39, 14, 799, DateTimeKind.Local).AddTicks(8617), null, "Admin", null },
                    { new Guid("bd49d739-e238-42a3-a55d-22c1ae23092c"), new DateTime(2024, 8, 31, 19, 39, 14, 799, DateTimeKind.Local).AddTicks(8735), null, "Receptionist", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                schema: "admin",
                table: "Roles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                schema: "admin",
                table: "Roles",
                column: "UserId",
                principalSchema: "admin",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
