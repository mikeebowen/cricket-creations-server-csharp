using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddBlogPostTagsBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagId",
                table: "BlogPostTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "BlogPost",
                type: "int",
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
    }
}
