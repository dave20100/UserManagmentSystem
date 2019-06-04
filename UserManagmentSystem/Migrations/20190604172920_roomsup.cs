using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagmentSystem.Migrations
{
    public partial class roomsup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Player1Name",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Player2Name",
                table: "Rooms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Name",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Player2Name",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);
        }
    }
}
