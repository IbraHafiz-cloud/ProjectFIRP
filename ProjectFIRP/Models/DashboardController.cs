using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFIRP.Data;
using ProjectFIRP.Models;
using System;
using System.Linq;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Total Barang Masuk
        var totalMasuk = _context.BarangMasuks.Sum(b => (int?)b.Jumlah) ?? 0;

        // Total Barang Keluar
        var totalKeluar = _context.BarangKeluars.Sum(b => (int?)b.Jumlah) ?? 0;

        // Gabungan untuk Stok
        var masuk = _context.BarangMasuks
            .GroupBy(x => x.NamaBarang)
            .Select(g => new
            {
                NamaObat = g.Key,
                TotalMasuk = g.Sum(x => x.Jumlah),
                Expired = g.Max(x => x.TanggalKadaluarsa),
                Keterangan = g.Select(x => x.Keterangan).FirstOrDefault()
            });

        var keluar = _context.BarangKeluars
            .GroupBy(x => x.NamaBarang)
            .Select(g => new
            {
                NamaObat = g.Key,
                TotalKeluar = g.Sum(x => x.Jumlah)
            });

        var stokGabungan = from m in masuk
                           join k in keluar on m.NamaObat equals k.NamaObat into gj
                           from k in gj.DefaultIfEmpty()
                           select new StokObatViewModel
                           {
                               NamaObat = m.NamaObat,
                               TotalMasuk = m.TotalMasuk,
                               TotalKeluar = k != null ? k.TotalKeluar : 0,
                               ExpiredTerbaru = m.Expired,
                               Keterangan = m.Keterangan
                           };

        var totalSisa = stokGabungan.Sum(x => x.StokSisa);
        var totalExpired = stokGabungan.Count(x => x.ExpiredTerbaru.HasValue && x.ExpiredTerbaru.Value < DateTime.Today);

        // Stok terendah
        ViewBag.StokRendah = stokGabungan?.OrderBy(x => x.StokSisa).Take(5).ToList() ?? new List<StokObatViewModel>();


        // Data untuk grafik
        var trend = _context.BarangMasuks
            .GroupBy(b => b.TanggalMasuk.Date)
            .Select(g => new
            {
                Tanggal = g.Key.ToString("dd/MM/yyyy"),
                Jumlah = g.Sum(x => x.Jumlah)
            }).OrderBy(x => x.Tanggal).ToList();

        ViewBag.TrendLabels = trend.Select(x => x.Tanggal).ToList();
        ViewBag.TrendData = trend.Select(x => x.Jumlah).ToList();

        // Cards
        ViewBag.TotalMasuk = totalMasuk;
        ViewBag.TotalKeluar = totalKeluar;
        ViewBag.TotalSisa = totalSisa;
        ViewBag.TotalKadaluarsa = totalExpired;

        return View();
    }
}
