using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class CreateInvoiceDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "SubscriptionUsers",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 2);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "Invoices",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "SubscriptionUsers",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Partners",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoices",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Invoices",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "Invoices",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));
        }
    }
}
