using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class RemoveRefreshTokenExpiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "User");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(4738), new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(5233) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6562), new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6572) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6576), new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6578) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6582), new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(6584) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(9406), new DateTime(2021, 2, 22, 17, 2, 13, 850, DateTimeKind.Local).AddTicks(9852) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 851, DateTimeKind.Local).AddTicks(289), new DateTime(2021, 2, 22, 17, 2, 13, 851, DateTimeKind.Local).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 2, 22, 17, 2, 13, 846, DateTimeKind.Local).AddTicks(3909), new DateTime(2021, 2, 22, 17, 2, 13, 848, DateTimeKind.Local).AddTicks(7049), "+lhlbpWTx55+H5vqGc9KCbDIvNay99777jFD0dEm1Mg=", new byte[] { 189, 169, 193, 152, 43, 120, 57, 179, 155, 238, 223, 175, 169, 14, 216, 239 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "User",
                type: "datetime2",
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
    }
}
