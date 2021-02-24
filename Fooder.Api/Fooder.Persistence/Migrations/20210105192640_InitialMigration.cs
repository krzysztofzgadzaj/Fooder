using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooder.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afflictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afflictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActivityLevel = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    BodyWeight = table.Column<float>(nullable: true),
                    HeightInCentimetres = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UniqueId = table.Column<Guid>(nullable: false),
                    BrandNameId = table.Column<int>(nullable: false),
                    FeedType = table.Column<string>(nullable: false),
                    MinWeight = table.Column<float>(nullable: false),
                    MaxWeight = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProducerSiteLink = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    DogActivityLevel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_Brands_BrandNameId",
                        column: x => x.BrandNameId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ProductMark = table.Column<float>(nullable: false),
                    CommentMark = table.Column<float>(nullable: false),
                    CommentEntityId = table.Column<int>(nullable: true),
                    FeedEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_CommentEntity_CommentEntityId",
                        column: x => x.CommentEntityId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentEntity_Feeds_FeedEntityId",
                        column: x => x.FeedEntityId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeedAffliction",
                columns: table => new
                {
                    FeedEntityId = table.Column<int>(nullable: false),
                    AfflictionEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedAffliction", x => new { x.FeedEntityId, x.AfflictionEntityId });
                    table.ForeignKey(
                        name: "FK_FeedAffliction_Afflictions_AfflictionEntityId",
                        column: x => x.AfflictionEntityId,
                        principalTable: "Afflictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedAffliction_Feeds_FeedEntityId",
                        column: x => x.FeedEntityId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    FeedEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Feeds_FeedEntityId",
                        column: x => x.FeedEntityId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Afflictions_Name",
                table: "Afflictions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CommentEntityId",
                table: "CommentEntity",
                column: "CommentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_FeedEntityId",
                table: "CommentEntity",
                column: "FeedEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedAffliction_AfflictionEntityId",
                table: "FeedAffliction",
                column: "AfflictionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_BrandNameId",
                table: "Feeds",
                column: "BrandNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Name",
                table: "Feeds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FeedEntityId",
                table: "Reviews",
                column: "FeedEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "FeedAffliction");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Afflictions");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
