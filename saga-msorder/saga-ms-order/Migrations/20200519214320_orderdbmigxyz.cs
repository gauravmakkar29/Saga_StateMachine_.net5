using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saga_BlockSeat.Migrations
{
    public partial class orderdbmigxyz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderData",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderData", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderData");
        }
    }
}
