using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vet_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VeterinarianSpecialtyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianSpecialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeterinarianId = table.Column<int>(type: "int", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianSpecialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinarianSpecialties_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinarianSpecialties_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General" },
                    { 2, "Medicina interna" },
                    { 3, "Cirugía" },
                    { 4, "Dermatología" },
                    { 5, "Oftalmología" },
                    { 6, "Oncología" },
                    { 7, "Cardiología" },
                    { 8, "Neurología" },
                    { 9, "Odontología" },
                    { 10, "Nutrición" },
                    { 11, "Rehabilitación" },
                    { 12, "Comportamiento" },
                    { 13, "Anestesiología" },
                    { 14, "Radiología" },
                    { 15, "Urgencias" },
                    { 16, "Patología" },
                    { 17, "Laboratorio" },
                    { 18, "Zootecnia" },
                    { 19, "Acupuntura" },
                    { 20, "Homeopatía" },
                    { 21, "Fisioterapia" },
                    { 22, "Ortopedia" },
                    { 23, "Endocrinología" },
                    { 24, "Geriatria" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_Name",
                table: "Specialties",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianSpecialties_SpecialtyId",
                table: "VeterinarianSpecialties",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianSpecialties_VeterinarianId",
                table: "VeterinarianSpecialties",
                column: "VeterinarianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeterinarianSpecialties");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
