using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class AddPageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Tag",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Heading = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(2669), new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(3125) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4343), new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4361) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4381), new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4383) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4387), new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(4389) });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Content", "Created", "Heading", "LastUpdated", "Title" },
                values: new object[,]
                {
                    { 1, "Bacon ipsum dolor amet strip steak bresaola chislic, bacon short loin kevin andouille brisket corned beef. Turducken spare ribs pork chop frankfurter, bresaola kielbasa meatball meatloaf pork chislic shoulder short loin leberkas. Frankfurter kevin bacon leberkas ham drumstick shankle flank t-bone biltong shank meatball pork chop bresaola turducken. Frankfurter bacon cupim, hamburger doner pork chop ribeye beef.", new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(6593), "The About Page", new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(6994), "About" },
                    { 2, "Fish tacos with cabbage slaw and a side of chips and guac. CARNITAS!! These tacos are lit 🔥. Can you put some peppers and onions on that? Black or pinto beans? Give me all the tacos, immediately. How bout a gosh darn quesadilla? Black or pinto beans? It’s taco time all the time. Um, Tabasco? No thanks, do you have any Cholula? It’s a wonderful morning for breakfast tacos. How do you feel about hard shelled tacos? Make it a double there pal. I’d have to say, those tacos are on fleek", new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(7424), "The Taco Page", new DateTime(2021, 1, 24, 14, 36, 3, 0, DateTimeKind.Local).AddTicks(7445), "Taco" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 14, 36, 2, 996, DateTimeKind.Local).AddTicks(9144), new DateTime(2021, 1, 24, 14, 36, 2, 998, DateTimeKind.Local).AddTicks(9206) });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CreatedBy",
                table: "Tag",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_CreatedBy",
                table: "Tag",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_CreatedBy",
                table: "Tag");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropIndex(
                name: "IX_Tag_CreatedBy",
                table: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Tag",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(1845), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3510), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3532) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3557), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3561) });

            migrationBuilder.UpdateData(
                table: "BlogPost",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3565), new DateTime(2021, 1, 24, 13, 11, 19, 395, DateTimeKind.Local).AddTicks(3569) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2021, 1, 24, 13, 11, 19, 391, DateTimeKind.Local).AddTicks(7629), new DateTime(2021, 1, 24, 13, 11, 19, 393, DateTimeKind.Local).AddTicks(7921) });
        }
    }
}
