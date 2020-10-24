using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class RemoveRequirdAttrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BlogPost",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "BlogPost",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 880, DateTimeKind.Local).AddTicks(7022), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(7178) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8624), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8645) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8665), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8671), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8674) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BlogPost",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "BlogPost",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 6, 19, 15, 51, 10, 359, DateTimeKind.Local).AddTicks(3005), new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(5729) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8196), new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8231) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8266), new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8271) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8276), new DateTime(2020, 6, 19, 15, 51, 10, 363, DateTimeKind.Local).AddTicks(8280) });
        }
    }
}
