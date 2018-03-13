using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class CreateProductsAndProductEditionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArabicName = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnglishName = table.Column<string>(nullable: false),
                    UniversalIP = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductEdition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArabicName = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnglishName = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UniversalIP = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEdition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEdition_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_EnglishName",
                table: "ProductEdition",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_Id",
                table: "ProductEdition",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEdition_ProductId",
                table: "ProductEdition",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_EnglishName",
                table: "Products",
                column: "EnglishName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id",
                table: "Products",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEdition");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
