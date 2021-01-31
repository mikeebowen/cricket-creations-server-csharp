using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddDeleteToBlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BlogPost",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "BlogPost",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "BlogPost");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 189, DateTimeKind.Local).AddTicks(9422), new DateTime(2021, 1, 31, 11, 38, 7, 189, DateTimeKind.Local).AddTicks(9866) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1046), new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1063) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1083), new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1086) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1090), new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(1092) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(3550), new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(3977) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(4412), new DateTime(2021, 1, 31, 11, 38, 7, 190, DateTimeKind.Local).AddTicks(4427) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 31, 11, 38, 7, 186, DateTimeKind.Local).AddTicks(5111), new DateTime(2021, 1, 31, 11, 38, 7, 188, DateTimeKind.Local).AddTicks(6069) });
        }
    }
}
