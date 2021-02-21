using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(8408), new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(8844) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9920), new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9929) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9967), new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9969) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9972), new DateTime(2021, 2, 21, 13, 13, 5, 133, DateTimeKind.Local).AddTicks(9974) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 134, DateTimeKind.Local).AddTicks(2219), new DateTime(2021, 2, 21, 13, 13, 5, 134, DateTimeKind.Local).AddTicks(2581) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 134, DateTimeKind.Local).AddTicks(2935), new DateTime(2021, 2, 21, 13, 13, 5, 134, DateTimeKind.Local).AddTicks(2943) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 2, 21, 13, 13, 5, 130, DateTimeKind.Local).AddTicks(6685), new DateTime(2021, 2, 21, 13, 13, 5, 132, DateTimeKind.Local).AddTicks(4711), "04Zz/22bJJPiyPRG/lns1ongLF3+Q72Vg41lYB8Vb/c=", new byte[] { 127, 207, 84, 72, 185, 76, 21, 122, 1, 76, 71, 204, 252, 140, 104, 137 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "User");

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
    }
}
