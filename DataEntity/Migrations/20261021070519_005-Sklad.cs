using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class _005Sklad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialId", "Datum", "Komentar", "MernaJednotkaId", "MnozDoPal", "MnozPoj", "Nazev" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Komentar k Vrutu 6", 1, 0, 0, "Vrut 6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 1);
        }
    }
}
