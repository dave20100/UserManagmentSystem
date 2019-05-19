using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagmentSystem.Migrations
{
    public partial class rankingpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankingPoints",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RankingPoints",
                table: "Users");
        }
    }
}
