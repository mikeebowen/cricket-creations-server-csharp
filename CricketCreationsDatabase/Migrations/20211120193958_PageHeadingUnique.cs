using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class PageHeadingUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "Page",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 11, 20, 11, 39, 57, 883, DateTimeKind.Local).AddTicks(9912), new DateTime(2021, 11, 20, 11, 39, 57, 885, DateTimeKind.Local).AddTicks(9270), "9AS4j9rrsZ/jgdUTALNajZUyisNiBvZ3JOT6SHxrmdI=", new byte[] { 26, 159, 230, 58, 142, 22, 220, 89, 141, 212, 186, 148, 29, 1, 58, 203 } });

            migrationBuilder.CreateIndex(
                name: "IX_Page_Heading",
                table: "Page",
                column: "Heading",
                unique: true,
                filter: "[Heading] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Page_Heading",
                table: "Page");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 11, 6, 11, 11, 1, 593, DateTimeKind.Local).AddTicks(3946), new DateTime(2021, 11, 6, 11, 11, 1, 595, DateTimeKind.Local).AddTicks(5694), "aaAEi/c8V9kFgYWp5I5de6cTfwTU+a34zemdmwa7SHw=", new byte[] { 1, 40, 20, 114, 201, 14, 135, 9, 248, 79, 231, 103, 245, 22, 165, 113 } });
        }
    }
}
