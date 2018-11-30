using System;
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
                    DateAdded = table.Column<DateTime>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Side = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    leavesQty = table.Column<long>(nullable: true),
                    LatestPrice = table.Column<double>(nullable: true),
                    lastLeavesQty = table.Column<long>(nullable: true),
                    deletedTime = table.Column<DateTime>(nullable: true)
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
