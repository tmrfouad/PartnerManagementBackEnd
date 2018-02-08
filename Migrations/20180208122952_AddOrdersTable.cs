using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class AddOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    apartment = table.Column<string>(nullable: true),
                    building = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    floor = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    postal_code = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    merchant_order_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    amount_cents = table.Column<float>(nullable: false),
                    currency = table.Column<string>(nullable: true),
                    delivery_needed = table.Column<bool>(nullable: false),
                    merchant_id = table.Column<int>(nullable: false),
                    shipping_dataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.merchant_order_id);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingData_shipping_dataId",
                        column: x => x.shipping_dataId,
                        principalTable: "ShippingData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipping_dataId",
                table: "Orders",
                column: "shipping_dataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShippingData");
        }
    }
}
