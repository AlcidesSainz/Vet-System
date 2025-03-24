using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vet_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Owners",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Owners");
        }
    }
}
