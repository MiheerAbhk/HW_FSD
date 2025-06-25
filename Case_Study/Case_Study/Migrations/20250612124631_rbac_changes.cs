using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class rbac_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Airlines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Airlines_OwnerId",
                table: "Airlines",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Users_OwnerId",
                table: "Airlines",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Users_OwnerId",
                table: "Airlines");

            migrationBuilder.DropIndex(
                name: "IX_Airlines_OwnerId",
                table: "Airlines");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Airlines");
        }
    }
}
