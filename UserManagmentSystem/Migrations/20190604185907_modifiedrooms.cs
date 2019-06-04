using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagmentSystem.Migrations
{
    public partial class modifiedrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Rooms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Rooms");
        }
    }
}
