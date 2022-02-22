using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hatchery.BigBank.Migrations
{
    public partial class RenamedToClosedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartneredTo",
                table: "Partners",
                newName: "ClosedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClosedAt",
                table: "Partners",
                newName: "PartneredTo");
        }
    }
}
