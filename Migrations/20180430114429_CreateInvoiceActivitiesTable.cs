using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class CreateInvoiceActivitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceActivity_Invoices_InvoiceId",
                table: "InvoiceActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceActivity",
                table: "InvoiceActivity");

            migrationBuilder.RenameTable(
                name: "InvoiceActivity",
                newName: "InvoiceActivities");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceActivity_InvoiceId",
                table: "InvoiceActivities",
                newName: "IX_InvoiceActivities_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceActivities",
                table: "InvoiceActivities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceActivities_Invoices_InvoiceId",
                table: "InvoiceActivities",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceActivities_Invoices_InvoiceId",
                table: "InvoiceActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceActivities",
                table: "InvoiceActivities");

            migrationBuilder.RenameTable(
                name: "InvoiceActivities",
                newName: "InvoiceActivity");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceActivities_InvoiceId",
                table: "InvoiceActivity",
                newName: "IX_InvoiceActivity_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceActivity",
                table: "InvoiceActivity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceActivity_Invoices_InvoiceId",
                table: "InvoiceActivity",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
