using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AvatarToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_AvatarId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_User_AvatarId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 11, 27, 14, 18, 32, 220, DateTimeKind.Local).AddTicks(4054), new DateTime(2021, 11, 27, 14, 18, 32, 222, DateTimeKind.Local).AddTicks(3297), "h+wCHC8NiObRtaLzMn96c0tEeHoJuZ97v+s6QrCYUzU=", new byte[] { 64, 33, 78, 129, 135, 56, 135, 151, 145, 170, 150, 206, 94, 2, 210, 217 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated", "Password", "Salt" },
                values: new object[] { new DateTime(2021, 11, 20, 11, 39, 57, 883, DateTimeKind.Local).AddTicks(9912), new DateTime(2021, 11, 20, 11, 39, 57, 885, DateTimeKind.Local).AddTicks(9270), "9AS4j9rrsZ/jgdUTALNajZUyisNiBvZ3JOT6SHxrmdI=", new byte[] { 26, 159, 230, 58, 142, 22, 220, 89, 141, 212, 186, 148, 29, 1, 58, 203 } });

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_AvatarId",
                table: "User",
                column: "AvatarId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
