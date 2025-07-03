using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFIRP.Data.Migrations
{
    /// <inheritdoc />
    public partial class TambahBarangMasuk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarangKeluars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaBarang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    TanggalKeluar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangKeluars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarangMasuks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaBarang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    TanggalMasuk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangMasuks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StokObats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaObat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    TanggalExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokObats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaksis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaBarang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    Jenis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarangKeluars");

            migrationBuilder.DropTable(
                name: "BarangMasuks");

            migrationBuilder.DropTable(
                name: "StokObats");

            migrationBuilder.DropTable(
                name: "Transaksis");
        }
    }
}
