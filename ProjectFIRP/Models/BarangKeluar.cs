using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectFIRP.Models
{
    public class BarangKeluar
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama barang wajib diisi")]
        [Display(Name = "Nama Barang")]
        public string NamaBarang { get; set; }

        [Required(ErrorMessage = "Jumlah wajib diisi")]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah harus lebih dari 0")]
        public int Jumlah { get; set; }

        [Required(ErrorMessage = "Tanggal keluar wajib diisi")]
        [Display(Name = "Tanggal Keluar")]
        public DateTime TanggalKeluar { get; set; }

        [Display(Name = "Keterangan (Opsional)")]
        public string? Keterangan { get; set; }
    }
}
