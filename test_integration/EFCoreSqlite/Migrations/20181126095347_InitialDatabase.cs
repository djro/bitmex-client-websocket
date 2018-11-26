using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSqlite.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Liquidations",
                columns: table => new
                {
                    OrderID = table.Column<string>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Side = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    leavesQty = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidations", x => x.OrderID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Liquidations");
        }
    }
}
