using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement_DAL.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
