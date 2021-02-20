using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(2438), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(2944) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4061), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4070) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4074), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4079), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(6448), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(6827) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(7241), new DateTime(2021, 2, 20, 11, 40, 13, 501, DateTimeKind.Local).AddTicks(7249) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 2, 20, 11, 40, 13, 498, DateTimeKind.Local).AddTicks(178), new DateTime(2021, 2, 20, 11, 40, 13, 499, DateTimeKind.Local).AddTicks(8041), "ghAtHLGDD3D5PxZimoLRQSV0miHu8mPPS7+v0jhMcGg=", new byte[] { 115, 18, 72, 33, 46, 112, 28, 126, 66, 111, 92, 173, 23, 158, 8, 161 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(6748), new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(7171) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8279), new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8289) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8293), new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8295) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8298), new DateTime(2021, 2, 20, 10, 23, 42, 934, DateTimeKind.Local).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 935, DateTimeKind.Local).AddTicks(632), new DateTime(2021, 2, 20, 10, 23, 42, 935, DateTimeKind.Local).AddTicks(1004) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 935, DateTimeKind.Local).AddTicks(1374), new DateTime(2021, 2, 20, 10, 23, 42, 935, DateTimeKind.Local).AddTicks(1382) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 931, DateTimeKind.Local).AddTicks(4452), new DateTime(2021, 2, 20, 10, 23, 42, 933, DateTimeKind.Local).AddTicks(2684), "oTMqggB3KTF80kPQUYH0xXOIouleh1YcHFSdrky6GlI=", new byte[] { 150, 67, 96, 65, 161, 187, 130, 11, 159, 218, 49, 215, 40, 183, 89, 252 } });
        }
    }
}
