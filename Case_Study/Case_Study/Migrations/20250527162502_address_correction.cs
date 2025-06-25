using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class address_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "address",
                table: "Users",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Users",
                newName: "address");
        }
    }
}
