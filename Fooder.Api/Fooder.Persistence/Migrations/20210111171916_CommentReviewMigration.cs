using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooder.Persistence.Migrations
{
    public partial class CommentReviewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentEntity_CommentEntity_CommentEntityId",
                table: "CommentEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentEntity_Feeds_FeedEntityId",
                table: "CommentEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentEntity",
                table: "CommentEntity");

            migrationBuilder.DropIndex(
                name: "IX_CommentEntity_CommentEntityId",
                table: "CommentEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "CommentEntityId",
                table: "CommentEntity");

            migrationBuilder.RenameTable(
                name: "CommentEntity",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_CommentEntity_FeedEntityId",
                table: "Comments",
                newName: "IX_Comments_FeedEntityId");

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Reviews",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Feeds",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Feeds",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "ProductMark",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "FeedEntityId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CommentMark",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "RelatedCommentId",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Comments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RelatedCommentId",
                table: "Comments",
                column: "RelatedCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Feeds_FeedEntityId",
                table: "Comments",
                column: "FeedEntityId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_RelatedCommentId",
                table: "Comments",
                column: "RelatedCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Feeds_FeedEntityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_RelatedCommentId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RelatedCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "RelatedCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "CommentEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_FeedEntityId",
                table: "CommentEntity",
                newName: "IX_CommentEntity_FeedEntityId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "ProductMark",
                table: "CommentEntity",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FeedEntityId",
                table: "CommentEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "CommentMark",
                table: "CommentEntity",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentEntityId",
                table: "CommentEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentEntity",
                table: "CommentEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CommentEntityId",
                table: "CommentEntity",
                column: "CommentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_CommentEntity_CommentEntityId",
                table: "CommentEntity",
                column: "CommentEntityId",
                principalTable: "CommentEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_Feeds_FeedEntityId",
                table: "CommentEntity",
                column: "FeedEntityId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
