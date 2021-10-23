using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddPublishedToPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Page",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(260), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(274) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1057), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1066) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1069), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1071) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1074), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(1076) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(3735), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(3745) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(3750), new DateTime(2021, 10, 23, 9, 36, 29, 247, DateTimeKind.Local).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 10, 23, 9, 36, 29, 243, DateTimeKind.Local).AddTicks(4019), new DateTime(2021, 10, 23, 9, 36, 29, 245, DateTimeKind.Local).AddTicks(5524), "zKTGCFYOGE7heSA5yv8vTmzUpCTfG3hEqQ1/O00FQUI=", new byte[] { 122, 164, 193, 19, 162, 39, 206, 182, 213, 8, 161, 215, 214, 149, 179, 25 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Page");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6260), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6274) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6660), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6668) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6672), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6674) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6677), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(6679) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(7788), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(7802), new DateTime(2021, 8, 3, 13, 42, 33, 261, DateTimeKind.Local).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 8, 3, 13, 42, 33, 258, DateTimeKind.Local).AddTicks(4955), new DateTime(2021, 8, 3, 13, 42, 33, 260, DateTimeKind.Local).AddTicks(5840), "n6Y+XMmYlhlrUEEdyopjJkApVSsmvEX3O55oJl3wyxw=", new byte[] { 214, 78, 130, 150, 19, 180, 152, 139, 237, 207, 168, 49, 90, 171, 165, 137 } });
        }
    }
}
