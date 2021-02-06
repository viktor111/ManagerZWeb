using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBManagerZ.Migrations
{
    public partial class AddSpentToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Spent",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spent",
                table: "Orders");
        }
    }
}
