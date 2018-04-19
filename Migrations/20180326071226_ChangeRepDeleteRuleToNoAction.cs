using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class ChangeRepDeleteRuleToNoAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction");

            migrationBuilder.AddForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction",
                column: "RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction");

            migrationBuilder.AddForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction",
                column: "RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
