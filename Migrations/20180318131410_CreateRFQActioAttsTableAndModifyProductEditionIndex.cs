using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class CreateRFQActioAttsTableAndModifyProductEditionIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEdition_EnglishName",
                table: "ProductEdition");

            migrationBuilder.DropIndex(
                name: "IX_ProductEdition_ProductId",
                table: "ProductEdition");

            migrationBuilder.CreateTable(
                name: "RFQActionAtt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    RFQActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQActionAtt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RFQActionAtt_RFQAction_RFQActionId",
                        column: x => x.RFQActionId,
                        principalTable: "RFQAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_ProductId_EnglishName",
                table: "ProductEdition",
                columns: new[] { "ProductId", "EnglishName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFQActionAtt_RFQActionId",
                table: "RFQActionAtt",
                column: "RFQActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFQActionAtt");

            migrationBuilder.DropIndex(
                name: "IX_ProductEdition_ProductId_EnglishName",
                table: "ProductEdition");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_EnglishName",
                table: "ProductEdition",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_ProductId",
                table: "ProductEdition",
                column: "ProductId");
        }
    }
}
