using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectFIRP.Models; // pastikan pakai namespace Models kamu

namespace ProjectFIRP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BarangMasuk> BarangMasuks { get; set; }
        public DbSet<BarangKeluar> BarangKeluars { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        //public DbSet<StokObatViewModel> StokObats { get; set; }
    }
}
