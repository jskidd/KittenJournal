using Microsoft.EntityFrameworkCore.Migrations;

namespace KittenJournal.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cincinnati",
                table: "Fosters");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Fosters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Fosters");

            migrationBuilder.AddColumn<string>(
                name: "Cincinnati",
                table: "Fosters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
