using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagmentSystem.Migrations
{
    public partial class roomsmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "timeControl",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "timeControlBonus",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "timeControl",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "timeControlBonus",
                table: "Rooms");
        }
    }
}
