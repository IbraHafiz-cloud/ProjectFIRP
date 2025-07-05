using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectFIRP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tambahkan DbSet jika kamu punya model tambahan (contoh):
        // public DbSet<Product> Products { get; set; }
        // public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}