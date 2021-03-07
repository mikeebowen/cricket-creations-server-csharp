using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddRefreshTokenExpiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(2460), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(2875) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3978), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3988) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3992), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3994) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3996), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(3999) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(6305), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(6677) });

            migrationBuilder.UpdateData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(7045), new DateTime(2021, 3, 7, 10, 52, 14, 77, DateTimeKind.Local).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 3, 7, 10, 52, 14, 73, DateTimeKind.Local).AddTicks(8736), new DateTime(2021, 3, 7, 10, 52, 14, 75, DateTimeKind.Local).AddTicks(7578), "Oga0daoBSd5COy1IN+aQE8ct0OlbNkGws7oQYpO9yGg=", new byte[] { 128, 229, 253, 184, 111, 87, 125, 96, 71, 53, 59, 65, 31, 5, 111, 140 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiration",
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
    }
}
