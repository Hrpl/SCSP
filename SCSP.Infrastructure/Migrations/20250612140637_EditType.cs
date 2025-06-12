using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCSP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "metadata_json",
                table: "project_files",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metadata_json",
                table: "project_files");
        }
    }
}
