using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeastBook_final.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: false),
                    NadKategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kategorie_Kategorie_NadKategoriaId",
                        column: x => x.NadKategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Przepisy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(maxLength: 40, nullable: false),
                    Hasztag = table.Column<string>(maxLength: 1000, nullable: false),
                    Ocena = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    Image2 = table.Column<byte[]>(nullable: true),
                    Image3 = table.Column<byte[]>(nullable: true),
                    Tresc = table.Column<string>(nullable: true),
                    KategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przepisy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Przepisy_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrzepisyProdukty",
                columns: table => new
                {
                    PrzepisId = table.Column<int>(nullable: false),
                    ProduktId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzepisyProdukty", x => new { x.PrzepisId, x.ProduktId });
                    table.ForeignKey(
                        name: "FK_PrzepisyProdukty_Produkty_ProduktId",
                        column: x => x.ProduktId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzepisyProdukty_Przepisy_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Przepisy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorie_NadKategoriaId",
                table: "Kategorie",
                column: "NadKategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Przepisy_KategoriaId",
                table: "Przepisy",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzepisyProdukty_ProduktId",
                table: "PrzepisyProdukty",
                column: "ProduktId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrzepisyProdukty");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Przepisy");

            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
