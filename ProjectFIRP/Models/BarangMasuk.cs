using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectFIRP.Models
{
    public class BarangMasuk
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Barang")]
        public string NamaBarang { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah harus lebih dari 0")]
        public int Jumlah { get; set; }

        [Required]
        [Display(Name = "Tanggal Masuk")]
        [DataType(DataType.Date)]
        public DateTime TanggalMasuk { get; set; }

        [Display(Name = "Tanggal Kadaluarsa")]
        [DataType(DataType.Date)]
        public DateTime? TanggalKadaluarsa { get; set; } // ✅ Tambahan

        [Display(Name = "Keterangan (Opsional)")]
        public string? Keterangan { get; set; }
    }
}
