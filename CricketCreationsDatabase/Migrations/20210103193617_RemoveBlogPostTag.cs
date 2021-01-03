using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class RemoveBlogPostTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "Tag",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "BlogPost",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 11, 36, 16, 701, DateTimeKind.Local).AddTicks(223), new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(2280) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3684), new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3704) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3723), new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3726) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3729), new DateTime(2021, 1, 3, 11, 36, 16, 703, DateTimeKind.Local).AddTicks(3731) });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_BlogPostId",
                table: "Tag",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_TagId",
                table: "BlogPost",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_Tag_TagId",
                table: "BlogPost",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_BlogPost_BlogPostId",
                table: "Tag",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_Tag_TagId",
                table: "BlogPost");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_BlogPost_BlogPostId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_BlogPostId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_TagId",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "BlogPost");

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.TagId, x.BlogPostId });
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

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_BlogPostId",
                table: "BlogPostTag",
                column: "BlogPostId");
        }
    }
}
