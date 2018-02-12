using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class AddNavigationPropertyOrderToShippingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_shipping_dataId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipping_dataId",
                table: "Orders",
                column: "shipping_dataId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_shipping_dataId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipping_dataId",
                table: "Orders",
                column: "shipping_dataId");
        }
    }
}
