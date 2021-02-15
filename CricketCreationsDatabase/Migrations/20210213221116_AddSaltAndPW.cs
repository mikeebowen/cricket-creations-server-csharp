using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddSaltAndPW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "User",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(2425), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(2875) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4165), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4184) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4204), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4206) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4209), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(4212) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(6710), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(7113) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(7600), new DateTime(2021, 2, 13, 14, 11, 15, 724, DateTimeKind.Local).AddTicks(7616) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 2, 13, 14, 11, 15, 720, DateTimeKind.Local).AddTicks(8104), new DateTime(2021, 2, 13, 14, 11, 15, 722, DateTimeKind.Local).AddTicks(7890), "eRBGPyhCtQv4OV+vJfZ4TphuZsG+dxEFuYhQC9OHqKw=", new byte[] { 186, 20, 91, 142, 56, 59, 238, 143, 231, 208, 155, 199, 89, 242, 137, 75 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "User");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(2718), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(3167) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4371), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4389) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4409), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4411) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4415), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(4417) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(7000), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(7408) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(7854), new DateTime(2021, 2, 13, 13, 52, 2, 551, DateTimeKind.Local).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 13, 13, 52, 2, 547, DateTimeKind.Local).AddTicks(7643), new DateTime(2021, 2, 13, 13, 52, 2, 549, DateTimeKind.Local).AddTicks(9011) });
        }
    }
}
