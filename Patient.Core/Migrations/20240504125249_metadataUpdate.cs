using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient.Core.Migrations
{
    /// <inheritdoc />
    public partial class metadataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "PrescribedOn",
                table: "Prescription",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "RefillCount",
                table: "Prescription",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Patient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescribedOn",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "RefillCount",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Patient");
        }
    }
}
