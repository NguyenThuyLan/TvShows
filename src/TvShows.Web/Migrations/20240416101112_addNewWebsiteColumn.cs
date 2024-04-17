using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShows.Web.Migrations
{
    /// <inheritdoc />
    public partial class addNewWebsiteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "website",
                table: "tvShowReviews",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "website",
                table: "tvShowReviews");
        }
    }
}
