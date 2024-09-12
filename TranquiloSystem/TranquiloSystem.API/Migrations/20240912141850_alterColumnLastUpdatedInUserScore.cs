using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranquiloSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class alterColumnLastUpdatedInUserScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "UserScores",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserScores",
                newName: "LastUpdated");
        }
    }
}
