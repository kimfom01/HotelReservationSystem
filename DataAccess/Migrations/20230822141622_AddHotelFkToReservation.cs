using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations;

/// <inheritdoc />
public partial class AddHotelFkToReservation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "HotelId",
            table: "Reservations",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Reservations_HotelId",
            table: "Reservations",
            column: "HotelId");

        migrationBuilder.AddForeignKey(
            name: "FK_Reservations_Hotels_HotelId",
            table: "Reservations",
            column: "HotelId",
            principalTable: "Hotels",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Reservations_Hotels_HotelId",
            table: "Reservations");

        migrationBuilder.DropIndex(
            name: "IX_Reservations_HotelId",
            table: "Reservations");

        migrationBuilder.DropColumn(
            name: "HotelId",
            table: "Reservations");
    }
}
