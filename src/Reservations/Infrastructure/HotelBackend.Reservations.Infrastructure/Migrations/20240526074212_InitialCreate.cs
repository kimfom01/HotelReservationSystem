using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBackend.Reservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reservations");

            migrationBuilder.CreateTable(
                name: "GuestProfiles",
                schema: "reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Adult = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", maxLength: 200, nullable: true),
                    SpecialRequests = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RoomPreferences = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NumberOfGuests = table.Column<int>(type: "integer", nullable: false),
                    GuestProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_GuestProfiles_GuestProfileId",
                        column: x => x.GuestProfileId,
                        principalSchema: "reservations",
                        principalTable: "GuestProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "reservations",
                table: "GuestProfiles",
                columns: new[] { "Id", "Adult", "Age", "ContactEmail", "CreatedAt", "FirstName", "LastModifiedAt", "LastName", "Sex" },
                values: new object[] { new Guid("91555d72-5259-433c-a597-23eeab1da9e3"), false, 0, "guestprofile@mail.com", new DateTime(2024, 5, 26, 10, 42, 11, 864, DateTimeKind.Local).AddTicks(9755), "Guest", null, "Profile", "" });

            migrationBuilder.InsertData(
                schema: "reservations",
                table: "Reservations",
                columns: new[] { "Id", "CheckIn", "CheckOut", "CreatedAt", "GuestProfileId", "HotelId", "LastModifiedAt", "NumberOfGuests", "PaymentId", "PaymentStatus", "ReservationStatus", "RoomId", "RoomPreferences", "SpecialRequests" },
                values: new object[] { new Guid("37dfb45a-77e8-4aa0-9c96-50209a772c90"), new DateTime(2024, 5, 26, 10, 42, 11, 864, DateTimeKind.Local).AddTicks(9952), new DateTime(2024, 5, 31, 10, 42, 11, 864, DateTimeKind.Local).AddTicks(9966), new DateTime(2024, 5, 26, 10, 42, 11, 864, DateTimeKind.Local).AddTicks(9932), new Guid("91555d72-5259-433c-a597-23eeab1da9e3"), new Guid("772e0735-5e83-4894-aa59-d5dc56105404"), null, 5, null, 0, 0, new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestProfileId",
                schema: "reservations",
                table: "Reservations",
                column: "GuestProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "reservations");

            migrationBuilder.DropTable(
                name: "GuestProfiles",
                schema: "reservations");
        }
    }
}
