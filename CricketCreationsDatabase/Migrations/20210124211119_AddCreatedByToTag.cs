using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddCreatedByToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Tag",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(1845), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3510), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3532) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3557), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3561) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3565), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3569) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 391, DateTimeKind.Local).AddTicks(7629), new DateTime(2021, 1, 24, 13, 11, 19, 393, DateTimeKind.Local).AddTicks(7921) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tag");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(7475), new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(7912) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9055), new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9075) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9098), new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9102) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9106), new DateTime(2021, 1, 24, 12, 8, 28, 349, DateTimeKind.Local).AddTicks(9109) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 12, 8, 28, 345, DateTimeKind.Local).AddTicks(3278), new DateTime(2021, 1, 24, 12, 8, 28, 347, DateTimeKind.Local).AddTicks(8210) });
        }
    }
}
