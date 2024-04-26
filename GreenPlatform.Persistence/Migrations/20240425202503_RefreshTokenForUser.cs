using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenPlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "GreenPlatformUser",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "GreenPlatformUser");
        }
    }
}
