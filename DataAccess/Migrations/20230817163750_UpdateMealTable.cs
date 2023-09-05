using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations;

/// <inheritdoc />
public partial class UpdateMealTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "HotelId",
            table: "Meals",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<decimal>(
            name: "MealPrice",
            table: "Meals",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.CreateIndex(
            name: "IX_Meals_HotelId",
            table: "Meals",
            column: "HotelId");

        migrationBuilder.AddForeignKey(
            name: "FK_Meals_Hotels_HotelId",
            table: "Meals",
            column: "HotelId",
            principalTable: "Hotels",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Meals_Hotels_HotelId",
            table: "Meals");

        migrationBuilder.DropIndex(
            name: "IX_Meals_HotelId",
            table: "Meals");

        migrationBuilder.DropColumn(
            name: "HotelId",
            table: "Meals");

        migrationBuilder.DropColumn(
            name: "MealPrice",
            table: "Meals");
    }
}
