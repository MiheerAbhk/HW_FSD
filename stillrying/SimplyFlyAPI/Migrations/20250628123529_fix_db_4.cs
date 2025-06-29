using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplyFlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class fix_db_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "FlightRoutes",
                newName: "Source");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Source",
                table: "FlightRoutes",
                newName: "Origin");
        }
    }
}
