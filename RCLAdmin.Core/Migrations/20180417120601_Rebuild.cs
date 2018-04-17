using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RCLAdmin.Core.Migrations
{
    public partial class Rebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrinterAccessoryType");

            migrationBuilder.DropTable(
                name: "PrinterEvent");

            migrationBuilder.DropTable(
                name: "PrinterAccessory");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropTable(
                name: "PrinterType");
        }
    }
}
