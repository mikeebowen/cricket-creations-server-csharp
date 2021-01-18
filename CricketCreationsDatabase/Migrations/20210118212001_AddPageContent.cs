using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddPageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostTag_BlogPostId",
                table: "BlogPostTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag",
                columns: new[] { "BlogPostId", "TagId" });

            migrationBuilder.CreateTable(
                name: "PageContent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 13, 40, 21, 650, DateTimeKind.Local).AddTicks(8814), new DateTime(2021, 1, 3, 13, 40, 21, 652, DateTimeKind.Local).AddTicks(9772) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1140), new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1161) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1181), new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1183) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1186), new DateTime(2021, 1, 3, 13, 40, 21, 653, DateTimeKind.Local).AddTicks(1189) });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_BlogPostId",
                table: "BlogPostTag",
                column: "BlogPostId");
        }
    }
}
