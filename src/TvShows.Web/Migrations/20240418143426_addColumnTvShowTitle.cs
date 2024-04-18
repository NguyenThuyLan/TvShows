using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShows.Web.Migrations
{
    /// <inheritdoc />
    public partial class addColumnTvShowTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tvShowTitle",
                table: "tvShowReviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tvShowTitle",
                table: "tvShowReviews");
        }
    }
}
