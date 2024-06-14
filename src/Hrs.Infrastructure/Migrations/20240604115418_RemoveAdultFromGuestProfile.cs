using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdultFromGuestProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adult",
                schema: "reservations",
                table: "GuestProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Adult",
                schema: "reservations",
                table: "GuestProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
