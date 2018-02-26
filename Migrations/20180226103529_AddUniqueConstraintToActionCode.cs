using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class AddUniqueConstraintToActionCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActionCode",
                table: "RFQAction",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_RFQAction_ActionCode",
                table: "RFQAction",
                column: "ActionCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RFQAction_ActionCode",
                table: "RFQAction");

            migrationBuilder.AlterColumn<string>(
                name: "ActionCode",
                table: "RFQAction",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
