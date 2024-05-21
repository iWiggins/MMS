using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medication.Core.Migrations
{
    /// <inheritdoc />
    public partial class BrandModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branded");

            migrationBuilder.RenameColumn(
                name: "medId",
                table: "Generic",
                newName: "MedId");

            migrationBuilder.RenameColumn(
                name: "manId",
                table: "Generic",
                newName: "ManId");

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MedId = table.Column<int>(type: "INTEGER", nullable: false),
                    ManId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.RenameColumn(
                name: "MedId",
                table: "Generic",
                newName: "medId");

            migrationBuilder.RenameColumn(
                name: "ManId",
                table: "Generic",
                newName: "manId");

            migrationBuilder.CreateTable(
                name: "Branded",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ManId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branded", x => x.Id);
                });
        }
    }
}
