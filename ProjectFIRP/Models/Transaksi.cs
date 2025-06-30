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
        [Display(Name = "Jumlah")]
        public int Jumlah { get; set; }

        [Required]
        [Display(Name = "Jenis Transaksi")]
        public string Jenis { get; set; } // Masuk / Keluar

        [Required]
        [Display(Name = "Tanggal Transaksi")]
        public DateTime Tanggal { get; set; }
    }
}
