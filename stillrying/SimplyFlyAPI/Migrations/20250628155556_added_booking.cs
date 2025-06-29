using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplyFlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class added_booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Bookings");
        }
    }
}
