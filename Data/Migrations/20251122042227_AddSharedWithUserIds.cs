using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VmoragaCollectionManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSharedWithUserIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SharedWithUserIds",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedWithUserIds",
                table: "Collections");
        }
    }
}
