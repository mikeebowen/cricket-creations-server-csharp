using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Tag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Page",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(4319), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(4771) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(5971), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(5991) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(6012), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(6015) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(6019), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(6022) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(8556), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(9424), new DateTime(2021, 1, 31, 13, 32, 18, 536, DateTimeKind.Local).AddTicks(9444) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 32, 18, 533, DateTimeKind.Local).AddTicks(998), new DateTime(2021, 1, 31, 13, 32, 18, 535, DateTimeKind.Local).AddTicks(942) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Page");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(2433), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(2904) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4167), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4187) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4208), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4212) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4218), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(4221) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(6790), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(7214) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(7682), new DateTime(2021, 1, 31, 13, 23, 22, 158, DateTimeKind.Local).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 13, 23, 22, 154, DateTimeKind.Local).AddTicks(7318), new DateTime(2021, 1, 31, 13, 23, 22, 156, DateTimeKind.Local).AddTicks(8239) });
        }
    }
}
