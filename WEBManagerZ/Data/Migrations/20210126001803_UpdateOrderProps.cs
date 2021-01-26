using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBManagerZ.Migrations
{
    public partial class UpdateOrderProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Orders");
        }
    }
}
