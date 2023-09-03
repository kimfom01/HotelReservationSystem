using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.Migrations
{
    /// <inheritdoc />
    public partial class RenameStatusToRoomStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Guests_GuestId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Reservations_ReservationId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Rooms_RoomId",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "RoomStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_RoomId",
                table: "RoomStatuses",
                newName: "IX_RoomStatuses_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_ReservationId",
                table: "RoomStatuses",
                newName: "IX_RoomStatuses_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_GuestId",
                table: "RoomStatuses",
                newName: "IX_RoomStatuses_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomStatuses",
                table: "RoomStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStatuses_Guests_GuestId",
                table: "RoomStatuses",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStatuses_Reservations_ReservationId",
                table: "RoomStatuses",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomStatuses_Rooms_RoomId",
                table: "RoomStatuses",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomStatuses_Guests_GuestId",
                table: "RoomStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStatuses_Reservations_ReservationId",
                table: "RoomStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomStatuses_Rooms_RoomId",
                table: "RoomStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomStatuses",
                table: "RoomStatuses");

            migrationBuilder.RenameTable(
                name: "RoomStatuses",
                newName: "Statuses");

            migrationBuilder.RenameIndex(
                name: "IX_RoomStatuses_RoomId",
                table: "Statuses",
                newName: "IX_Statuses_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomStatuses_ReservationId",
                table: "Statuses",
                newName: "IX_Statuses_ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomStatuses_GuestId",
                table: "Statuses",
                newName: "IX_Statuses_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Guests_GuestId",
                table: "Statuses",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Reservations_ReservationId",
                table: "Statuses",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Rooms_RoomId",
                table: "Statuses",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
