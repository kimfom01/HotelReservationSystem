using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Admin.Infrastructure.Database.Migrations.Admin
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");

            migrationBuilder.CreateTable(
                name: "Hotels",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RoomPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTypes_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "admin",
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "admin",
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomNumber = table.Column<string>(type: "text", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "admin",
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalSchema: "admin",
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                schema: "admin",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "admin",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "admin",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                schema: "admin",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                schema: "admin",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_HotelId",
                schema: "admin",
                table: "RoomTypes",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HotelId",
                schema: "admin",
                table: "Users",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                schema: "admin",
                table: "UsersRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "UsersRoles",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "RoomTypes",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Hotels",
                schema: "admin");
        }
    }
}
