using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Vet_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClinicTableLocationModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clinics");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Clinics",
                type: "geography",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Clinics");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clinics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
