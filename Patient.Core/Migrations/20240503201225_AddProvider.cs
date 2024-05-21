using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProviderId",
                table: "Prescription",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Prescription");
        }
    }
}
