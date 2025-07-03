using System;

namespace ProjectFIRP.Models
{
    public class StokObatViewModel
    {
        public string NamaObat { get; set; }
        public int TotalMasuk { get; set; }
        public int TotalKeluar { get; set; }
        public int StokSisa => TotalMasuk - TotalKeluar;
        public DateTime? ExpiredTerbaru { get; set; }
        public string? Keterangan { get; set; }
    }
}
