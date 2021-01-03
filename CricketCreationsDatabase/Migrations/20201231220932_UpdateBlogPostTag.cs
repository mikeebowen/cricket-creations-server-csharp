using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class UpdateBlogPostTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogPostTag_TagId",
                table: "BlogPostTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostTag",
                table: "BlogPostTag",
                columns: new[] { "TagId", "BlogPostId" });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 31, 14, 9, 32, 251, DateTimeKind.Local).AddTicks(6012), new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(6903) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8250), new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8269) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8289), new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8294), new DateTime(2020, 12, 31, 14, 9, 32, 253, DateTimeKind.Local).AddTicks(8296) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(2020, 12, 30, 17, 47, 50, 684, DateTimeKind.Local).AddTicks(977), new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(1809) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3199), new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3219) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3239), new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3242) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3245), new DateTime(2020, 12, 30, 17, 47, 50, 686, DateTimeKind.Local).AddTicks(3247) });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagId",
                table: "BlogPostTag",
                column: "TagId");
        }
    }
}
