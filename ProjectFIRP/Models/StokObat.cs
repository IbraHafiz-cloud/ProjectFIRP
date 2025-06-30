using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectFIRP.Models
{
    public class StokObat
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Obat")]
        public string NamaObat { get; set; }

        [Required]
        [Display(Name = "Jumlah Stok")]
        public int Jumlah { get; set; }

        [Display(Name = "Tanggal Expired")]
        [DataType(DataType.Date)]
        public DateTime? TanggalExpired { get; set; }

        [Display(Name = "Keterangan")]
        public string? Keterangan { get; set; }
    }
}
