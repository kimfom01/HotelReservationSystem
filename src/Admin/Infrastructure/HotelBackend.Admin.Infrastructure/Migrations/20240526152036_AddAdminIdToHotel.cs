using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBackend.Admin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminIdToHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                schema: "admin",
                table: "Hotels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"),
                columns: new[] { "AdminId", "CreatedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9662) });

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                columns: new[] { "AdminId", "CreatedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9614) });

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9909));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("323dafa4-5aaa-48df-aff2-bbb084660335"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(143));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("408198cb-8118-4e42-990b-558046ab4785"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(129));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("4cd15491-e564-44b5-b2d4-645864be718f"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(114));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9953));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e2ec684-008c-41c0-a2a7-c80e70f6fe41"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(100));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9939));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("95afd44e-3478-4d98-855b-5b541dc00005"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9924));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("98355d46-005c-4aa5-948b-cb43a52821a8"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(171));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("d9c54399-f818-4e06-8983-fd997d95346c"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("e401adcc-7f91-4375-8574-6f5c035861df"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(157));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9823));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9851));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("491a8c7e-3211-476e-a3af-e8113582cb32"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(5));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("59a47955-4957-4140-8a19-92868033a0d7"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(48));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9790));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("6e0e6e38-6937-44e6-93b3-b68fdf8864e5"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(67));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("711027f4-d0f1-4005-8acc-8cbb35228d7e"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(34));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9809));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 805, DateTimeKind.Local).AddTicks(9837));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("efe8c495-c2d7-4a1e-ad07-d7ee546215eb"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 18, 20, 35, 806, DateTimeKind.Local).AddTicks(20));

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HotelId",
                schema: "admin",
                table: "Employees",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                schema: "admin",
                table: "Employees",
                column: "HotelId",
                principalSchema: "admin",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Hotels_HotelId",
                schema: "admin",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_HotelId",
                schema: "admin",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AdminId",
                schema: "admin",
                table: "Hotels");

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("2efb57c9-cecd-45b9-997c-4dda8400f460"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5805));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Hotels",
                keyColumn: "Id",
                keyValue: new Guid("772e0735-5e83-4894-aa59-d5dc56105404"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5762));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("2cf8abf4-a9df-48b6-b261-132b3de15cd9"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6062));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("323dafa4-5aaa-48df-aff2-bbb084660335"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6286));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("408198cb-8118-4e42-990b-558046ab4785"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6272));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("4cd15491-e564-44b5-b2d4-645864be718f"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6258));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("59acf344-ff6b-4f8a-8bcd-80131d6a8e57"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6101));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e2ec684-008c-41c0-a2a7-c80e70f6fe41"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6244));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e569fcc-2ef2-42c6-bc5f-fc534afc7616"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6091));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("8454a407-1140-47f6-89d8-de6b9e9ad4cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6115));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("95afd44e-3478-4d98-855b-5b541dc00005"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6077));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("98355d46-005c-4aa5-948b-cb43a52821a8"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6314));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("d9c54399-f818-4e06-8983-fd997d95346c"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6047));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: new Guid("e401adcc-7f91-4375-8574-6f5c035861df"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6300));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("10dfd202-183e-4f50-9903-2777ce05fbb9"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5980));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("21cdd2ec-164d-416a-a9fe-51d4444d13d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("491a8c7e-3211-476e-a3af-e8113582cb32"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6158));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("59a47955-4957-4140-8a19-92868033a0d7"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("6d2ee634-519f-48df-b701-b789aa5599a0"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5952));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("6e0e6e38-6937-44e6-93b3-b68fdf8864e5"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6210));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("711027f4-d0f1-4005-8acc-8cbb35228d7e"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6186));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b0b061d8-6c60-4079-963b-1e13b7d4ed35"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5966));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b2c3c1e7-de5e-43cf-865f-b40ab6554d66"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(5990));

            migrationBuilder.UpdateData(
                schema: "admin",
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("efe8c495-c2d7-4a1e-ad07-d7ee546215eb"),
                column: "CreatedAt",
                value: new DateTime(2024, 5, 26, 10, 42, 22, 495, DateTimeKind.Local).AddTicks(6172));
        }
    }
}
