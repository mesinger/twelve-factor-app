using Microsoft.EntityFrameworkCore.Migrations;

namespace BurgerkingCaloriesCalculator.WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_menus",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    product_ids = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_menus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_menus");
        }
    }
}
