using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class RemoveIdFromAttachmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RFQActionAtt",
                table: "RFQActionAtt");

            migrationBuilder.DropIndex(
                name: "IX_RFQActionAtt_RFQActionId",
                table: "RFQActionAtt");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RFQActionAtt");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "RFQActionAtt",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RFQActionAtt",
                table: "RFQActionAtt",
                columns: new[] { "RFQActionId", "FileName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RFQActionAtt",
                table: "RFQActionAtt");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "RFQActionAtt",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RFQActionAtt",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RFQActionAtt",
                table: "RFQActionAtt",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RFQActionAtt_RFQActionId",
                table: "RFQActionAtt",
                column: "RFQActionId");
        }
    }
}
