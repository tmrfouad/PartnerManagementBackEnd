using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class ChangeRfqStatusToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE RFQs SET Status = '0' WHERE Status = 'New'");
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "RFQs",
                nullable: false,
                oldClrType: typeof(int));
            migrationBuilder.Sql("UPDATE RFQs SET Status = 'New' WHERE Status = '0'");
        }
    }
}
