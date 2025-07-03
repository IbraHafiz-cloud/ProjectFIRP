using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFIRP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransaksiModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TanggalKadaluarsa",
                table: "Transaksis",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TanggalKadaluarsa",
                table: "Transaksis");
        }
    }
}
