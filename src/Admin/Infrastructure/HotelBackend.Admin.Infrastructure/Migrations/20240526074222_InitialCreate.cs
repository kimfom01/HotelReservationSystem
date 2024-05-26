using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBackend.Admin.Infrastructure.Migrations
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
                name: "Employees",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                schema: "admin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.InsertData(
                schema: "admin",
                table: "Hotels",
                columns: new[] { "Id", "CreatedAt", "LastModifiedAt", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5805), null, "Moscow, Russia", "Moscow Hotel" },
                    { new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5762), null, "Voronezh, Russia", "Voronezh Hotel" }
                });

            migrationBuilder.InsertData(
                schema: "admin",
                table: "RoomTypes",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Description", "HotelId", "LastModifiedAt", "RoomPrice", "Type" },
                values: new object[,]
                {
                    { new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"), 1, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6062), "Larger than a standard room with additional seating area and possibly a better view.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 150m, "Deluxe" },
                    { new Guid("323dafa4-5aaa-48df-aff2-bbb084660335"), 3, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6286), "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 300m, "Suite" },
                    { new Guid("408198cb-8118-4e42-990b-558046ab4785"), 2, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6272), "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 200m, "Executive" },
                    { new Guid("4cd15491-e564-44b5-b2d4-645864be718f"), 1, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6258), "Larger than a standard room with additional seating area and possibly a better view.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 150m, "Deluxe" },
                    { new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"), 4, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6101), "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 350m, "Family" },
                    { new Guid("5e2ec684-008c-41c0-a2a7-c80e70f6fe41"), 1, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6244), "Basic room with essential amenities, ideal for budget travelers.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 100m, "Standard" },
                    { new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"), 3, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6091), "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 300m, "Suite" },
                    { new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"), 5, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6115), "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 500m, "Penthouse" },
                    { new Guid("95afd44e-3478-4d98-855b-5b541dc00005"), 2, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6077), "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 200m, "Executive" },
                    { new Guid("98355d46-005c-4aa5-948b-cb43a52821a8"), 5, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6314), "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 500m, "Penthouse" },
                    { new Guid("d9c54399-f818-4e06-8983-fd997d95346c"), 1, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6047), "Basic room with essential amenities, ideal for budget travelers.", new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 100m, "Standard" },
                    { new Guid("e401adcc-7f91-4375-8574-6f5c035861df"), 4, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6300), "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.", new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, 350m, "Family" }
                });

            migrationBuilder.InsertData(
                schema: "admin",
                table: "Rooms",
                columns: new[] { "Id", "Availability", "CreatedAt", "HotelId", "LastModifiedAt", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5980), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, "3E", new Guid("95afd44e-3478-4d98-855b-5b541dc00005") },
                    { new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6004), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, "2P", new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf") },
                    { new Guid("491a8c7e-3211-476e-a3af-e8113582cb32"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6158), new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, "1S", new Guid("5e2ec684-008c-41c0-a2a7-c80e70f6fe41") },
                    { new Guid("59a47955-4957-4140-8a19-92868033a0d7"), false, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6200), new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, "1F", new Guid("e401adcc-7f91-4375-8574-6f5c035861df") },
                    { new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5952), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, "1S", new Guid("d9c54399-f818-4e06-8983-fd997d95346c") },
                    { new Guid("6e0e6e38-6937-44e6-93b3-b68fdf8864e5"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6210), new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, "2P", new Guid("98355d46-005c-4aa5-948b-cb43a52821a8") },
                    { new Guid("711027f4-d0f1-4005-8acc-8cbb35228d7e"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6186), new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, "3E", new Guid("408198cb-8118-4e42-990b-558046ab4785") },
                    { new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5966), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, "2D", new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9") },
                    { new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"), false, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5990), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, "1F", new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57") },
                    { new Guid("efe8c495-c2d7-4a1e-ad07-d7ee546215eb"), true, new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6172), new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), null, "2D", new Guid("4cd15491-e564-44b5-b2d4-645864be718f") }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "RoomTypes",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "Hotels",
                schema: "admin");
        }
    }
}
