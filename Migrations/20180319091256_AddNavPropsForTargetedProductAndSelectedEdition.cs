using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class AddNavPropsForTargetedProductAndSelectedEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEdition",
                table: "ProductEdition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEdition",
                table: "ProductEdition",
                columns: new[] { "ProductId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_RFQs_TargetedProductId_SelectedEditionId",
                table: "RFQs",
                columns: new[] { "TargetedProductId", "SelectedEditionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RFQs_Products_TargetedProductId",
                table: "RFQs",
                column: "TargetedProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RFQs_ProductEdition_TargetedProductId_SelectedEditionId",
                table: "RFQs",
                columns: new[] { "TargetedProductId", "SelectedEditionId" },
                principalTable: "ProductEdition",
                principalColumns: new[] { "ProductId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFQs_Products_TargetedProductId",
                table: "RFQs");

            migrationBuilder.DropForeignKey(
                name: "FK_RFQs_ProductEdition_TargetedProductId_SelectedEditionId",
                table: "RFQs");

            migrationBuilder.DropIndex(
                name: "IX_RFQs_TargetedProductId_SelectedEditionId",
                table: "RFQs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEdition",
                table: "ProductEdition");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEdition",
                table: "ProductEdition",
                column: "Id");
        }
    }
}
