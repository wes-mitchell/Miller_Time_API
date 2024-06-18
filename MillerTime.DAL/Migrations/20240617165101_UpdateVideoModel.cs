using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MillerTime.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVideoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmbedUrl",
                table: "Videos",
                newName: "YoutubeVideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YoutubeVideoId",
                table: "Videos",
                newName: "EmbedUrl");
        }
    }
}
