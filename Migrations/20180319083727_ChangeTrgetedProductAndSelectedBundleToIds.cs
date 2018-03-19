using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class ChangeTrgetedProductAndSelectedBundleToIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetedProduct",
                table: "RFQs",
                newName: "TargetedProductId");

            migrationBuilder.RenameColumn(
                name: "SelectedBundle",
                table: "RFQs",
                newName: "SelectedEditionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetedProductId",
                table: "RFQs",
                newName: "TargetedProduct");

            migrationBuilder.RenameColumn(
                name: "SelectedEditionId",
                table: "RFQs",
                newName: "SelectedBundle");
        }
    }
}
