using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AboutUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "GreenPlatformUser",
                type: "text",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "GreenPlatformUser");
        }
    }
}
