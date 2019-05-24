using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineQueuing.Migrations
{
    public partial class addMigrationSzabi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointments");
        }
    }
}
