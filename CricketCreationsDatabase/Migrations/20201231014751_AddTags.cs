using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(nullable: false),
                    BlogPostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPost_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BlogPostTag_BlogPostId",
                table: "BlogPostTag",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagId",
                table: "BlogPostTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 880, DateTimeKind.Local).AddTicks(7022), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(7178) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8624), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8645) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8665), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8671), new DateTime(2020, 10, 24, 14, 20, 34, 882, DateTimeKind.Local).AddTicks(8674) });
        }
    }
}
