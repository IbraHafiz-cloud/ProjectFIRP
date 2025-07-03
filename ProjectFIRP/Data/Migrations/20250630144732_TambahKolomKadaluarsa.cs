using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFIRP.Data.Migrations
{
    /// <inheritdoc />
    public partial class TambahKolomKadaluarsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StokObats");

            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalKadaluarsa",
                table: "BarangMasuks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TanggalKadaluarsa",
                table: "BarangMasuks");

            migrationBuilder.CreateTable(
                name: "StokObats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jumlah = table.Column<int>(type: "int", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamaObat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalExpired = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokObats", x => x.Id);
                });
        }
    }
}
