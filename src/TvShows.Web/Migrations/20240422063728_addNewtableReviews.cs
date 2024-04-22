using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvShows.Web.Migrations
{
    /// <inheritdoc />
    public partial class addNewtableReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tvShowTitle = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    isApproved = table.Column<bool>(type: "INTEGER", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    tvShowKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    memberKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
