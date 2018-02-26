using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class AddUniqueIndexToRFQCodeAndContactPersonEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContactPersonEmail",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_RFQs_ContactPersonEmail",
                table: "RFQs",
                column: "ContactPersonEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFQs_RFQCode",
                table: "RFQs",
                column: "RFQCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RFQs_ContactPersonEmail",
                table: "RFQs");

            migrationBuilder.DropIndex(
                name: "IX_RFQs_RFQCode",
                table: "RFQs");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPersonEmail",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
