using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShows.Web.Migrations
{
    /// <inheritdoc />
    public partial class addNewColumnToChangeDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tvShowKey",
                table: "tvShowReviews");

            migrationBuilder.RenameColumn(
                name: "memberId",
                table: "tvShowReviews",
                newName: "MemberId");

            migrationBuilder.AddColumn<Guid>(
                name: "memberKeyId",
                table: "tvShowReviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tvShowKeyId",
                table: "tvShowReviews",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "memberKeyId",
                table: "tvShowReviews");

            migrationBuilder.DropColumn(
                name: "tvShowKeyId",
                table: "tvShowReviews");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "tvShowReviews",
                newName: "memberId");

            migrationBuilder.AddColumn<Guid>(
                name: "tvShowKey",
                table: "tvShowReviews",
                type: "TEXT",
                nullable: true);
        }
    }
}
