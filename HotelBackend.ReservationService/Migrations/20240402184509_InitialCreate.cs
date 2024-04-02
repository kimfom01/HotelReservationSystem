using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBackend.ReservationService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomNumber = table.Column<string>(type: "text", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: true),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SpecialRequests = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RoomPreferences = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NumberOfGuests = table.Column<int>(type: "integer", nullable: false),
                    GuestProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_GuestProfiles_GuestProfileId",
                        column: x => x.GuestProfileId,
                        principalTable: "GuestProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GuestProfiles",
                columns: new[] { "Id", "ContactEmail", "FirstName", "LastName" },
                values: new object[] { new Guid("91555d72-5259-433c-a597-23eeab1da9e3"), "guestprofile@mail.com", "Guest", "Profile" });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), "Moscow, Russia", "Moscow Hotel" },
                    { new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), "Voronezh, Russia", "Voronezh Hotel" }
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Capacity", "Description", "Type" },
                values: new object[,]
                {
                    { new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"), 1, "Larger than a standard room with additional seating area and possibly a better view.", "Deluxe" },
                    { new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"), 4, "Tailored for families, these suites offer multiple bedrooms or a larger space with sofa beds and sometimes kitchenettes.", "Family" },
                    { new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"), 3, "A luxurious, spacious room with separate living and sleeping areas, often featuring upscale amenities.", "Suite" },
                    { new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"), 5, "The pinnacle of luxury, located on the top floor with panoramic views, high-end furnishings, and exclusive amenities.", "Penthouse" },
                    { new Guid("95afd44e-3478-4d98-855b-5b541dc00005"), 2, "Designed for business travelers, often including a work desk, high-speed internet, and access to an executive lounge.", "Executive" },
                    { new Guid("d9c54399-f818-4e06-8983-fd997d95346c"), 1, "Basic room with essential amenities, ideal for budget travelers.", "Standard" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "RoomTypeId", "Value" },
                values: new object[,]
                {
                    { new Guid("41eb8604-8018-4689-a1e1-c9c0ceb45cb0"), new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"), 500m },
                    { new Guid("57f6c20f-25e4-4bc4-bbfe-55f310081878"), new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"), 350m },
                    { new Guid("5d6611bb-24db-4d73-b2bc-13b515fb49df"), new Guid("95afd44e-3478-4d98-855b-5b541dc00005"), 200m },
                    { new Guid("7c8d7dc1-2b29-446f-9575-a108129528b8"), new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"), 150m },
                    { new Guid("d11b6257-1cb0-44ab-95d7-d4e46def9c15"), new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"), 300m },
                    { new Guid("ec9783a1-96b0-4734-99ac-38639dda1c35"), new Guid("d9c54399-f818-4e06-8983-fd997d95346c"), 100m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Availability", "HotelId", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"), true, new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), "3E", new Guid("95afd44e-3478-4d98-855b-5b541dc00005") },
                    { new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"), true, new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), "2B", new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf") },
                    { new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"), true, new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), "1S", new Guid("d9c54399-f818-4e06-8983-fd997d95346c") },
                    { new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"), true, new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), "2D", new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9") },
                    { new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"), false, new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"), "1Q", new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57") }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckIn", "CheckOut", "CreationDate", "GuestProfileId", "HotelId", "NumberOfGuests", "PaymentId", "PaymentStatus", "ReservationStatus", "RoomId", "RoomPreferences", "SpecialRequests" },
                values: new object[] { new Guid("37dfb45a-77e8-4aa0-9c96-50209a772c90"), new DateTime(2024, 4, 2, 21, 45, 8, 952, DateTimeKind.Local).AddTicks(765), new DateTime(2024, 4, 7, 21, 45, 8, 952, DateTimeKind.Local).AddTicks(779), new DateTime(2024, 4, 2, 21, 45, 8, 952, DateTimeKind.Local).AddTicks(702), new Guid("91555d72-5259-433c-a597-23eeab1da9e3"), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), 5, null, 1, 0, new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_RoomTypeId",
                table: "Prices",
                column: "RoomTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestProfileId",
                table: "Reservations",
                column: "GuestProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelId",
                table: "Reservations",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "GuestProfiles");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomType");
        }
    }
}
