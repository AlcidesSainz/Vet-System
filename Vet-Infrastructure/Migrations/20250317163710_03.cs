using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vet_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Breeds_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BreedId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Species",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Perro" },
                    { 2, "Gato" },
                    { 3, "Ave" },
                    { 4, "Conejo" },
                    { 5, "Hámster" },
                    { 6, "Pez" }
                });

            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "Id", "Name", "SpeciesId" },
                values: new object[,]
                {
                    { 1, "Labrador Retriever", 1 },
                    { 2, "Bulldog", 1 },
                    { 3, "Pastor Alemán", 1 },
                    { 4, "Golden Retriever", 1 },
                    { 5, "Poodle", 1 },
                    { 6, "Chihuahua", 1 },
                    { 7, "Rottweiler", 1 },
                    { 8, "Yorkshire Terrier", 1 },
                    { 9, "Boxer", 1 },
                    { 10, "Dachshund", 1 },
                    { 11, "Shih Tzu", 1 },
                    { 12, "Schnauzer", 1 },
                    { 13, "Pug", 1 },
                    { 14, "Doberman", 1 },
                    { 15, "Beagle", 1 },
                    { 16, "Border Collie", 1 },
                    { 17, "Bichón Frisé", 1 },
                    { 18, "Husky Siberiano", 1 },
                    { 19, "Pastor Belga", 1 },
                    { 20, "Maltés", 1 },
                    { 21, "Siamés", 2 },
                    { 22, "Persa", 2 },
                    { 23, "Maine Coon", 2 },
                    { 24, "Bengalí", 2 },
                    { 25, "Sphynx", 2 },
                    { 26, "British Shorthair", 2 },
                    { 27, "Scottish Fold", 2 },
                    { 28, "Ragdoll", 2 },
                    { 29, "American Shorthair", 2 },
                    { 30, "Exótico de Pelo Corto", 2 },
                    { 31, "Azul Ruso", 2 },
                    { 32, "Birmano", 2 },
                    { 33, "Himalayo", 2 },
                    { 34, "Angora Turco", 2 },
                    { 35, "Abisinio", 2 },
                    { 36, "Ocicat", 2 },
                    { 37, "Bombay", 2 },
                    { 38, "Chartreux", 2 },
                    { 39, "Devon Rex", 2 },
                    { 40, "Cornish Rex", 2 },
                    { 41, "Periquito", 3 },
                    { 42, "Canario", 3 },
                    { 43, "Agapornis (Inseparable)", 3 },
                    { 44, "Ninfa (Cockatiel)", 3 },
                    { 45, "Lorito Australiano", 3 },
                    { 46, "Guacamayo", 3 },
                    { 47, "Loro Amazónico", 3 },
                    { 48, "Cacatúa", 3 },
                    { 49, "Periquito de Bourke", 3 },
                    { 50, "Diamante Mandarín", 3 },
                    { 51, "Diamante Gould", 3 },
                    { 52, "Cotorra", 3 },
                    { 53, "Yaco (Loro Gris Africano)", 3 },
                    { 54, "Loro Eclectus", 3 },
                    { 55, "Periquito de Línea", 3 },
                    { 56, "Conure del Sol", 3 },
                    { 57, "Conure Cabeza Negra", 3 },
                    { 58, "Agapornis Fischer", 3 },
                    { 59, "Agapornis Roseicollis", 3 },
                    { 60, "Periquito Ondulado", 3 },
                    { 61, "Holland Lop", 4 },
                    { 62, "Netherland Dwarf", 4 },
                    { 63, "Rex", 4 },
                    { 64, "Cabeza de León", 4 },
                    { 65, "Belier Francés", 4 },
                    { 66, "Californiano", 4 },
                    { 67, "Dutch Rabbit", 4 },
                    { 68, "Flemish Giant", 4 },
                    { 69, "Hotot Enano", 4 },
                    { 70, "Mini Lop", 4 },
                    { 71, "American Fuzzy Lop", 4 },
                    { 72, "English Angora", 4 },
                    { 73, "English Spot", 4 },
                    { 74, "Harlequin", 4 },
                    { 75, "Tan Rabbit", 4 },
                    { 76, "Satin Rabbit", 4 },
                    { 77, "Cinnamon Rabbit", 4 },
                    { 78, "Chinchilla Rabbit", 4 },
                    { 79, "Plata de Champagne", 4 },
                    { 80, "Gigante de España", 4 },
                    { 81, "Hámster Sirio Dorado", 5 },
                    { 82, "Hámster Sirio Panda", 5 },
                    { 83, "Hámster Sirio de Pelo Largo", 5 },
                    { 84, "Hámster Enano Ruso (Winter White)", 5 },
                    { 85, "Hámster Enano de Campbell", 5 },
                    { 86, "Hámster Enano Roborowski", 5 },
                    { 87, "Hámster Chino", 5 },
                    { 88, "Hámster Sirio Satinado", 5 },
                    { 89, "Hámster Sirio Bicolor", 5 },
                    { 90, "Hámster Sirio Black Bear", 5 },
                    { 91, "Hámster Sirio Tortoiseshell", 5 },
                    { 92, "Hámster Sirio Dalmatian", 5 },
                    { 93, "Hámster Sirio Calico", 5 },
                    { 94, "Hámster Sirio Chocolate", 5 },
                    { 95, "Hámster Sirio Cream", 5 },
                    { 96, "Hámster Sirio Albino", 5 },
                    { 97, "Hámster Enano Campbell Albino", 5 },
                    { 98, "Hámster Enano Ruso Zafiro", 5 },
                    { 99, "Hámster Enano Ruso Perla", 5 },
                    { 100, "Hámster Chino Rayado", 5 },
                    { 101, "Betta", 6 },
                    { 102, "Guppy", 6 },
                    { 103, "Molly", 6 },
                    { 104, "Platy", 6 },
                    { 105, "Tetra Neón", 6 },
                    { 106, "Tetra Cardenal", 6 },
                    { 107, "Pez Ángel (Escalar)", 6 },
                    { 108, "Disco", 6 },
                    { 109, "Goldfish", 6 },
                    { 110, "Carpa Koi", 6 },
                    { 111, "Cíclido Óscar", 6 },
                    { 112, "Corydora", 6 },
                    { 113, "Locha Payaso", 6 },
                    { 114, "Gourami Enano", 6 },
                    { 115, "Pez Arcoíris Boesemani", 6 },
                    { 116, "Barbo Tigre", 6 },
                    { 117, "Pez Payaso (Amphiprion ocellaris)", 6 },
                    { 118, "Pez Globo Enano", 6 },
                    { 119, "Killifish", 6 },
                    { 120, "Pez Gato Plecostomus", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_SpeciesId",
                table: "Breeds",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BreedId",
                table: "Pets",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets",
                column: "SpeciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
