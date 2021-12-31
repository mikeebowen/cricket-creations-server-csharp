using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddSubtitleToBlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "BlogPost",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 12, 30, 15, 28, 39, 517, DateTimeKind.Local).AddTicks(9753), new DateTime(2021, 12, 30, 15, 28, 39, 519, DateTimeKind.Local).AddTicks(9609), "q4zNoVSkxQ4DBJ/RFdEXgDKRf4KfijJXCLD2iG/u2XU=", new byte[] { 196, 108, 143, 162, 76, 208, 238, 6, 97, 237, 21, 170, 64, 241, 182, 103 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "BlogPost");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 12, 22, 13, 5, 0, 491, DateTimeKind.Local).AddTicks(9414), new DateTime(2021, 12, 22, 13, 5, 0, 493, DateTimeKind.Local).AddTicks(9178), "4QEdf1Z+cc2Vq5i/5BIea01dOhm81oNA39+T8OyeQhk=", new byte[] { 122, 133, 175, 255, 48, 234, 177, 67, 119, 168, 192, 126, 172, 43, 211, 228 } });
        }
    }
}
