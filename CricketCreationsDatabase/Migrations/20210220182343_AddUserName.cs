using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "User",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "User",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "Created", "LastUpdated", "Password", "Salt", "UserName" },
                values: new object[] { new DateTime(2021, 2, 20, 10, 23, 42, 931, DateTimeKind.Local).AddTicks(4452), new DateTime(2021, 2, 20, 10, 23, 42, 933, DateTimeKind.Local).AddTicks(2684), "oTMqggB3KTF80kPQUYH0xXOIouleh1YcHFSdrky6GlI=", new byte[] { 150, 67, 96, 65, 161, 187, 130, 11, 159, 218, 49, 215, 40, 183, 89, 252 }, "tacocat" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

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
    }
}
