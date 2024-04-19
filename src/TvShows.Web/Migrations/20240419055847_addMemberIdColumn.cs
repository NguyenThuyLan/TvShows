using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShows.Web.Migrations
{
    /// <inheritdoc />
    public partial class addMemberIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "website",
                table: "tvShowReviews");

            migrationBuilder.AddColumn<Guid>(
                name: "memberId",
                table: "tvShowReviews",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "memberId",
                table: "tvShowReviews");

            migrationBuilder.AddColumn<string>(
                name: "website",
                table: "tvShowReviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
