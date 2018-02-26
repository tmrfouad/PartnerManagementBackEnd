using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class ReaddUniqueToRFQCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RFQCode",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_RFQs_RFQCode",
                table: "RFQs",
                column: "RFQCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RFQs_RFQCode",
                table: "RFQs");

            migrationBuilder.AlterColumn<string>(
                name: "RFQCode",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
