using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Data;
using ProjectFIRP.Models;
using System.Linq;

public class StokObatController : Controller
{
    private readonly ApplicationDbContext _context;

    public StokObatController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var masuk = _context.BarangMasuks
            .GroupBy(x => x.NamaBarang)
            .Select(g => new
            {
                NamaObat = g.Key,
                TotalMasuk = g.Sum(x => x.Jumlah),
                // ✅ Ambil max TanggalKadaluarsa secara aman
                Expired = g.Where(x => x.TanggalKadaluarsa.HasValue)
                           .Select(x => x.TanggalKadaluarsa.Value)
                           .DefaultIfEmpty()
                           .Max(),
                Keterangan = g.Select(x => x.Keterangan).FirstOrDefault()
            })
            .ToList(); // Pastikan query ini dieksekusi dulu sebelum digunakan di LINQ selanjutnya

        var keluar = _context.BarangKeluars
            .GroupBy(x => x.NamaBarang)
            .Select(g => new
            {
                NamaObat = g.Key,
                TotalKeluar = g.Sum(x => x.Jumlah)
            })
            .ToList(); // Eksekusi query agar data siap

        var stok = from m in masuk
                   join k in keluar on m.NamaObat equals k.NamaObat into gj
                   from k in gj.DefaultIfEmpty()
                   select new StokObatViewModel
                   {
                       NamaObat = m.NamaObat,
                       TotalMasuk = m.TotalMasuk,
                       TotalKeluar = k?.TotalKeluar ?? 0,
                       ExpiredTerbaru = m.Expired,
                       Keterangan = m.Keterangan
                   };

        return View(stok.ToList());
    }
}
