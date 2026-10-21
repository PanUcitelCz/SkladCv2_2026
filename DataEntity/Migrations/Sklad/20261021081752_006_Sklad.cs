using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataEntity.Migrations.Sklad
{
    /// <inheritdoc />
    public partial class _006_Sklad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MernaJednotka",
                columns: table => new
                {
                    MernaJednotkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Popis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MernaJednotka", x => x.MernaJednotkaId);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MnozPoj = table.Column<int>(type: "int", nullable: false),
                    MnozDoPal = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "date", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MernaJednotkaId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_Material_MernaJednotka_MernaJednotkaId",
                        column: x => x.MernaJednotkaId,
                        principalTable: "MernaJednotka",
                        principalColumn: "MernaJednotkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paleta",
                columns: table => new
                {
                    PaletaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    Stav = table.Column<int>(type: "int", nullable: false),
                    AdresaUlozeni = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Mnozstvi = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: true),
                    Komentar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paleta", x => x.PaletaId);
                    table.ForeignKey(
                        name: "FK_Paleta_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId");
                });

            migrationBuilder.InsertData(
                table: "MernaJednotka",
                columns: new[] { "MernaJednotkaId", "DatumVytvoreni", "Popis" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 21, 10, 17, 51, 941, DateTimeKind.Local).AddTicks(1469), "ks" },
                    { 2, new DateTime(2026, 10, 21, 10, 17, 51, 942, DateTimeKind.Local).AddTicks(5912), "kg" },
                    { 3, new DateTime(2026, 10, 21, 10, 17, 51, 942, DateTimeKind.Local).AddTicks(5925), "m3" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialId", "Datum", "DatumVytvoreni", "Komentar", "MernaJednotkaId", "MnozDoPal", "MnozPoj", "Nazev" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Komentar k Vrutu 6", 1, 0, 0, "Vrut 6" });

            migrationBuilder.CreateIndex(
                name: "IX_Material_MernaJednotkaId",
                table: "Material",
                column: "MernaJednotkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Paleta_MaterialId",
                table: "Paleta",
                column: "MaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paleta");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "MernaJednotka");
        }
    }
}
