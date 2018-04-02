using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace acscustomersgatebackend.Migrations
{
    public partial class RemoveVideoConferenceFromActionTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE RFQAction SET ActionType = 2 WHERE ActionType = 4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
