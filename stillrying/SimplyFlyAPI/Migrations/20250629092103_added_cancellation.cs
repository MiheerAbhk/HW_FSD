using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplyFlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class added_cancellation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRefunded",
                table: "Cancellations");

            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "Cancellations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "Cancellations");

            migrationBuilder.AddColumn<bool>(
                name: "IsRefunded",
                table: "Cancellations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
