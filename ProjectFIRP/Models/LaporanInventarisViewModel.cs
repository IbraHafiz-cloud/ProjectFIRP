namespace ProjectFIRP.Models
{
    public class LaporanInventarisViewModel
    {
        public string NamaBarang { get; set; }
        public int TotalMasuk { get; set; }
        public int TotalKeluar { get; set; }

        public int StokAkhir => TotalMasuk - TotalKeluar;
    }
}
