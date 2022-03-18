using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddResetCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetCode",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpiration",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Email", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2022, 3, 14, 16, 50, 58, 750, DateTimeKind.Local).AddTicks(8365), "michael@example.com", new DateTime(2022, 3, 14, 16, 50, 58, 753, DateTimeKind.Local).AddTicks(421), "Tb6tqwRaBXWANRYn9bikXNXXx51PuZXzFTdorvRJHHo=", new byte[] { 160, 166, 86, 10, 131, 62, 244, 211, 47, 190, 166, 73, 144, 59, 160, 47 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetCodeExpiration",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Email", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 12, 31, 10, 46, 54, 357, DateTimeKind.Local).AddTicks(3584), "f@ff.com", new DateTime(2021, 12, 31, 10, 46, 54, 360, DateTimeKind.Local).AddTicks(1827), "B7ExtvKv1ga1GQdE8QS2x96JBwjbqSt5lCARm5I5scw=", new byte[] { 224, 116, 203, 217, 77, 142, 115, 161, 95, 85, 150, 207, 22, 14, 82, 57 } });
        }
    }
}
