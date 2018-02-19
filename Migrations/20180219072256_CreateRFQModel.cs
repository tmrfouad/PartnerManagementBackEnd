using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class CreateRFQModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RFQs",
                columns: table => new
                {
                    RFQId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CompanyArabicName = table.Column<string>(nullable: true),
                    CompanyEnglishName = table.Column<string>(nullable: true),
                    ContactPersonArabicName = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true),
                    ContactPersonEnglishName = table.Column<string>(nullable: true),
                    ContactPersonMobile = table.Column<string>(nullable: true),
                    ContactPersonPosition = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    RFQCode = table.Column<int>(nullable: false),
                    SelectedBundle = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SubmissionTime = table.Column<DateTime>(nullable: false),
                    TargetedProduct = table.Column<string>(nullable: true),
                    UniversalIP = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQs", x => x.RFQId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFQs");
        }
    }
}
