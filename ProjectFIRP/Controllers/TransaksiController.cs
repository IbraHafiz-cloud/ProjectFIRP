using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Data;
using ProjectFIRP.Models;
using System.Linq;

public class TransaksiController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransaksiController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var transaksiList = _context.Transaksis.ToList();
        return View(transaksiList); // akan cari file Views/Transaksi/Index.cshtml
    }

    public IActionResult Create()
    {
        return View(); // akan cari file Views/Transaksi/Create.cshtml
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Transaksi transaksi)
    {
        if (ModelState.IsValid)
        {
            _context.Transaksis.Add(transaksi);

            if (transaksi.Jenis == "Masuk")
            {
                var barangMasuk = new BarangMasuk
                {
                    NamaBarang = transaksi.NamaBarang,
                    Jumlah = transaksi.Jumlah,
                    TanggalMasuk = transaksi.Tanggal,
                    TanggalKadaluarsa = transaksi.TanggalKadaluarsa,
                    Keterangan = "Dari Transaksi"
                };
                _context.BarangMasuks.Add(barangMasuk);
            }
            else if (transaksi.Jenis == "Keluar")
            {
                var barangKeluar = new BarangKeluar
                {
                    NamaBarang = transaksi.NamaBarang,
                    Jumlah = transaksi.Jumlah,
                    TanggalKeluar = transaksi.Tanggal,
                    Keterangan = "Dari Transaksi"
                };
                _context.BarangKeluars.Add(barangKeluar);
            }

            _context.SaveChanges();
            TempData["success"] = "Transaksi berhasil disimpan.";
            return RedirectToAction(nameof(Index));
        }

        return View(transaksi);
    }
}
