using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PartnerManagement.Migrations
{
    public partial class ChangeRfqTagetedProductAndSelectedBundleTypeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Products ON");
            migrationBuilder.Sql("INSERT INTO Products (Id, EnglishName, ArabicName, Created, UniversalIP) SELECT 1, 'Process Perfect', N'بروسيس بيرفكت', GETDATE(), '0.0.0.0' WHERE NOT EXISTS(SELECT Id FROM Products WHERE Id = 1)");
            migrationBuilder.Sql("SET IDENTITY_INSERT Products OFF");

            migrationBuilder.Sql("SET IDENTITY_INSERT ProductEdition ON");
            migrationBuilder.Sql("INSERT INTO ProductEdition (ProductId, Id, EnglishName, ArabicName, Created, UniversalIP) SELECT 1, 1, 'Lite', N'لايت', GETDATE(), '0.0.0.0' WHERE NOT EXISTS(SELECT Id FROM ProductEdition WHERE ProductId = 1 AND Id = 1)");
            migrationBuilder.Sql("INSERT INTO ProductEdition (ProductId, Id, EnglishName, ArabicName, Created, UniversalIP) SELECT 1, 2, 'Standard', N'ستاندارد', GETDATE(), '0.0.0.0' WHERE NOT EXISTS(SELECT Id FROM ProductEdition WHERE ProductId = 1 AND Id = 2)");
            migrationBuilder.Sql("INSERT INTO ProductEdition (ProductId, Id, EnglishName, ArabicName, Created, UniversalIP) SELECT 1, 3, 'Enterprise', N'انتربرايز', GETDATE(), '0.0.0.0' WHERE NOT EXISTS(SELECT Id FROM ProductEdition WHERE ProductId = 1 AND Id = 3)");
            migrationBuilder.Sql("SET IDENTITY_INSERT ProductEdition OFF");

            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = '1' WHERE TargetedProduct = 'Process Perfect' AND SelectedBundle = 'Lite'");
            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = '2' WHERE TargetedProduct = 'Process Perfect' AND SelectedBundle = 'Standard'");
            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = '3' WHERE TargetedProduct = 'Process Perfect' AND SelectedBundle = 'Enterprise'");
            migrationBuilder.Sql("UPDATE RFQs SET TargetedProduct = '1' WHERE TargetedProduct = 'Process Perfect'");

            migrationBuilder.AlterColumn<int>(
                name: "TargetedProduct",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "SelectedBundle",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TargetedProduct",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SelectedBundle",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = 'Lite' WHERE TargetedProduct = 1 AND SelectedBundle = '1'");
            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = 'Standard' WHERE TargetedProduct = 1 AND SelectedBundle = '2'");
            migrationBuilder.Sql("UPDATE RFQs SET SelectedBundle = 'Enterprise' WHERE TargetedProduct = 1 AND SelectedBundle = '3'");
            migrationBuilder.Sql("UPDATE RFQs SET TargetedProduct = 'Process Perfect' WHERE TargetedProduct = '1'");
        }
    }
}
