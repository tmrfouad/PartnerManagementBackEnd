using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class ChangeRfqActionRepToRepIdAndChangeTypeToIntAndCreateRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyRepresentative",
                table: "RFQAction");

            migrationBuilder.AddColumn<int>(
                name: "RepresentativeId",
                table: "RFQAction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RFQAction_RepresentativeId",
                table: "RFQAction",
                column: "RepresentativeId");

            migrationBuilder.Sql("SET IDENTITY_INSERT Representatives ON");
            migrationBuilder.Sql("INSERT INTO Representatives (Id, Name, Address, Created, UniversalIP) SELECT 0, 'Company Represintitive', 'Test Address', GETDATE(), '0.0.0.0' WHERE NOT EXISTS(SELECT * FROM Representatives WHERE Id = 0)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Representatives OFF");

            migrationBuilder.AddForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction",
                column: "RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RFQAction_Representatives_RepresentativeId",
                table: "RFQAction");

            migrationBuilder.DropIndex(
                name: "IX_RFQAction_RepresentativeId",
                table: "RFQAction");

            migrationBuilder.DropColumn(
                name: "RepresentativeId",
                table: "RFQAction");

            migrationBuilder.AddColumn<string>(
                name: "CompanyRepresentative",
                table: "RFQAction",
                nullable: false,
                defaultValue: "");
        }
    }
}
