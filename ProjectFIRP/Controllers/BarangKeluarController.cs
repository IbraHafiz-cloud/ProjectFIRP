using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;
using ProjectFIRP.Data;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectFIRP.Controllers
{
    public class BarangKeluarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarangKeluarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BarangKeluar
        public IActionResult Index()
        {
            var dataList = _context.BarangKeluars
                .OrderByDescending(x => x.TanggalKeluar)
                .ToList();
            return View(dataList); // ✅ Model yg sesuai
        }

        // POST: BarangKeluar (Create)
        [HttpPost]
        public async Task<IActionResult> Index(BarangKeluar model)
        {
            if (ModelState.IsValid)
            {
                _context.BarangKeluars.Add(model);
                await _context.SaveChangesAsync();
                TempData["success"] = "Barang keluar berhasil dicatat.";
                return RedirectToAction("Index");
            }

            // Jika gagal validasi, tetap kembalikan list data
            var dataList = _context.BarangKeluars
                .OrderByDescending(x => x.TanggalKeluar)
                .ToList();
            return View(dataList);
        }

        // POST: Edit BarangKeluar
        [HttpPost]
        public async Task<IActionResult> Edit(BarangKeluar model)
        {
            if (ModelState.IsValid)
            {
                _context.BarangKeluars.Update(model);
                await _context.SaveChangesAsync();
                TempData["success"] = "Data berhasil diperbarui.";
            }
            return RedirectToAction("Index");
        }

        // POST: Delete BarangKeluar
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BarangKeluars.FindAsync(id);
            if (item != null)
            {
                _context.BarangKeluars.Remove(item);
                await _context.SaveChangesAsync();
                TempData["success"] = "Data berhasil dihapus.";
            }
            return RedirectToAction("Index");
        }
    }
}
