using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectFIRP.Models
{
    public class Transaksi
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Barang")]
        public string NamaBarang { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah harus lebih dari 0")]
        public int Jumlah { get; set; }

        [Required]
        [Display(Name = "Jenis Transaksi")]
        public string Jenis { get; set; } // "Masuk" atau "Keluar"

        [Required]
        [DataType(DataType.Date)]
        public DateTime Tanggal { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Kadaluarsa (Opsional)")]
        public DateTime? TanggalKadaluarsa { get; set; } // Hanya dipakai jika "Masuk"
    }
}
