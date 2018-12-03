using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSqlite.Migrations
{
    public partial class BitmexDb : Migration
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
                    numUpdates = table.Column<int>(nullable: true),
                    deletedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidations", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    Side = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TickDirection = table.Column<string>(nullable: true),
                    TrdMatchId = table.Column<string>(nullable: true),
                    GrossValue = table.Column<long>(nullable: true),
                    HomeNotional = table.Column<double>(nullable: true),
                    ForeignNotional = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => new { x.Timestamp, x.Symbol });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Liquidations");

            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
