using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranquiloSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class modifyUserAppPostComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "PostComments");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Posts",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "UpVoteCount");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "PostComments",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "PostComments",
                newName: "UpVoteCount");

            migrationBuilder.AddColumn<string>(
                name: "DownVoteCount",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "PostText",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DownVoteCount",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVoteCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "DownVoteCount",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UpVoteCount",
                table: "Posts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Posts",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpVoteCount",
                table: "PostComments",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "PostComments",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<int>(
                name: "Comments",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "PostComments",
                type: "datetime2",
                nullable: true);
        }
    }
}
