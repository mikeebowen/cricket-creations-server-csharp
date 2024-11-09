using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class TagHasManyUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_UserId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_UserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "TagUser",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagUser", x => new { x.TagsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TagUser_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Email", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 12, 31, 10, 46, 54, 357, DateTimeKind.Local).AddTicks(3584), "f@ff.com", new DateTime(2021, 12, 31, 10, 46, 54, 360, DateTimeKind.Local).AddTicks(1827), "B7ExtvKv1ga1GQdE8QS2x96JBwjbqSt5lCARm5I5scw=", new byte[] { 224, 116, 203, 217, 77, 142, 115, 161, 95, 85, 150, 207, 22, 14, 82, 57 } });

            migrationBuilder.CreateIndex(
                name: "IX_TagUser_UsersId",
                table: "TagUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Email", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 12, 30, 15, 28, 39, 517, DateTimeKind.Local).AddTicks(9753), "michael@example.com", new DateTime(2021, 12, 30, 15, 28, 39, 519, DateTimeKind.Local).AddTicks(9609), "q4zNoVSkxQ4DBJ/RFdEXgDKRf4KfijJXCLD2iG/u2XU=", new byte[] { 196, 108, 143, 162, 76, 208, 238, 6, 97, 237, 21, 170, 64, 241, 182, 103 } });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserId",
                table: "Tag",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_UserId",
                table: "Tag",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
