using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewsAPI.Migrations
{
    public partial class AbraKadabra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aboba",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aboba",
                table: "Games");
        }
    }
}
