using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class RemoveUnusedPageContentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageContent");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tag",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Tag",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "PageContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageContent", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 18, 13, 20, 1, 484, DateTimeKind.Local).AddTicks(1173), new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(943) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2318), new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2336) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2358), new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2361) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2364), new DateTime(2021, 1, 18, 13, 20, 1, 486, DateTimeKind.Local).AddTicks(2366) });
        }
    }
}
