using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VeteriansAndOtherTablesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlLogo",
                table: "Clinics",
                newName: "UrlImage");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clinics",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Veterinarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicVeterinarians",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    VeterinarianId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicVeterinarians", x => new { x.VeterinarianId, x.ClinicId });
                    table.ForeignKey(
                        name: "FK_ClinicVeterinarians_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicVeterinarians_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_Email",
                table: "Clinics",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_Phone",
                table: "Clinics",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicVeterinarians_ClinicId",
                table: "ClinicVeterinarians",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_Email",
                table: "Veterinarians",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_LicenseNumber",
                table: "Veterinarians",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_Phone",
                table: "Veterinarians",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicVeterinarians");

            migrationBuilder.DropTable(
                name: "Veterinarians");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_Email",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_Phone",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "Clinics",
                newName: "UrlLogo");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
