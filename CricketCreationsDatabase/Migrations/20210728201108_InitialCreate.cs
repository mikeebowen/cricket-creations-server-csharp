using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CricketCreationsDatabase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Image_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    BlogPostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.BlogPostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPost_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Created", "Deleted", "Email", "LastUpdated", "Name", "Password", "RefreshToken", "RefreshTokenExpiration", "Role", "Salt", "Surname", "UserName" },
                values: new object[] { 1, new DateTime(2021, 7, 28, 13, 11, 8, 567, DateTimeKind.Local).AddTicks(5631), false, "michael@example.com", new DateTime(2021, 7, 28, 13, 11, 8, 569, DateTimeKind.Local).AddTicks(5824), "Michael", "YuoAKU/Mqlu3hSMvl4CGr0A9HSeb43GJtiN+wbOCdLQ=", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new byte[] { 192, 39, 149, 55, 155, 61, 227, 28, 181, 255, 92, 169, 78, 60, 11, 150 }, "Test", "tacocat" });

            migrationBuilder.InsertData(
                table: "BlogPost",
                columns: new[] { "Id", "Content", "Created", "Deleted", "Image", "LastUpdated", "Published", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(6635), false, null, new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(6891), false, "enim neque volutpat ac tincidunt", 1 },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7549), false, null, new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7557), false, "volutpat odio facilisis mauris sit", 1 },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7560), false, null, new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7562), false, "maecenas volutpat blandit aliquam etiam", 1 },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Odio facilisis mauris sit amet massa vitae tortor condimentum lacinia. Lobortis scelerisque fermentum dui faucibus in. Faucibus ornare suspendisse sed nisi lacus sed viverra. Vulputate enim nulla aliquet porttitor lacus. Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. Dignissim enim sit amet venenatis urna. Consequat id porta nibh venenatis cras sed felis eget velit. Amet cursus sit amet dictum sit amet justo. Sit amet risus nullam eget felis eget nunc lobortis mattis. Dui sapien eget mi proin sed libero. Ullamcorper malesuada proin libero nunc consequat interdum. Nunc consequat interdum varius sit amet mattis vulputate enim. Lacus vestibulum sed arcu non odio. Ullamcorper a lacus vestibulum sed arcu non. Duis at tellus at urna. Donec massa sapien faucibus et molestie ac feugiat sed lectus. Faucibus scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam.", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7565), false, null, new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(7567), false, "viverra mauris in aliquam sem", 1 }
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "Content", "Created", "Deleted", "Heading", "LastUpdated", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Bacon ipsum dolor amet strip steak bresaola chislic, bacon short loin kevin andouille brisket corned beef. Turducken spare ribs pork chop frankfurter, bresaola kielbasa meatball meatloaf pork chislic shoulder short loin leberkas. Frankfurter kevin bacon leberkas ham drumstick shankle flank t-bone biltong shank meatball pork chop bresaola turducken. Frankfurter bacon cupim, hamburger doner pork chop ribeye beef.", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(9103), false, "The About Page", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(9345), "About", 1 },
                    { 2, "Fish tacos with cabbage slaw and a side of chips and guac. CARNITAS!! These tacos are lit 🔥. Can you put some peppers and onions on that? Black or pinto beans? Give me all the tacos, immediately. How bout a gosh darn quesadilla? Black or pinto beans? It’s taco time all the time. Um, Tabasco? No thanks, do you have any Cholula? It’s a wonderful morning for breakfast tacos. How do you feel about hard shelled tacos? Make it a double there pal. I’d have to say, those tacos are on fleek", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(9700), false, "The Taco Page", new DateTime(2021, 7, 28, 13, 11, 8, 570, DateTimeKind.Local).AddTicks(9708), "Taco", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_UserId",
                table: "BlogPost",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagsId",
                table: "BlogPostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId1",
                table: "Image",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Page_UserId",
                table: "Page",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserId",
                table: "Tag",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
