using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class AddPartnerRelatedTablesAndEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    PartnerId = table.Column<int>(nullable: false),
                    ProductEditionId = table.Column<int>(nullable: false),
                    ProductEditionId1 = table.Column<int>(nullable: true),
                    ProductEditionProductId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_ProductEdition_ProductEditionProductId_ProductEditionId1",
                        columns: x => new { x.ProductEditionProductId, x.ProductEditionId1 },
                        principalTable: "ProductEdition",
                        principalColumns: new[] { "ProductId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    Paid = table.Column<bool>(nullable: false, defaultValue: false),
                    Price = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Status = table.Column<int>(nullable: false, defaultValue: 2),
                    SubscriptionId = table.Column<int>(nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    SubscriptionId = table.Column<int>(nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionUsers_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activity = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: false),
                    UniversalIP = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceActivity_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceActivity_InvoiceId",
                table: "InvoiceActivity",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNo",
                table: "Invoices",
                column: "InvoiceNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SubscriptionId",
                table: "Invoices",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Email",
                table: "Partners",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_Name",
                table: "Partners",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PartnerId",
                table: "Subscriptions",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ProductId",
                table: "Subscriptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ProductEditionProductId_ProductEditionId1",
                table: "Subscriptions",
                columns: new[] { "ProductEditionProductId", "ProductEditionId1" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUsers_SubscriptionId_Name",
                table: "SubscriptionUsers",
                columns: new[] { "SubscriptionId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceActivity");

            migrationBuilder.DropTable(
                name: "SubscriptionUsers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
