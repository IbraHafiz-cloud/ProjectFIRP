using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;
using ProjectFIRP.Data;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectFIRP.Controllers
{
    public class BarangMasukController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarangMasukController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listBarang = _context.BarangMasuks.ToList();
            return View(listBarang);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BarangMasuk model)
        {
            if (ModelState.IsValid)
            {
                _context.BarangMasuks.Add(model);
                await _context.SaveChangesAsync();
                TempData["success"] = "Barang masuk berhasil ditambahkan.";
                return RedirectToAction("Index");
            }

            var listBarang = _context.BarangMasuks.ToList();
            return View(listBarang);
        }

        // ======== Tambahan Fitur Edit ========
        public IActionResult Edit(int id)
        {
            var barang = _context.BarangMasuks.FirstOrDefault(x => x.Id == id);
            if (barang == null)
                return NotFound();

            return View(barang);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BarangMasuk model)
        {
            if (ModelState.IsValid)
            {
                _context.BarangMasuks.Update(model);
                await _context.SaveChangesAsync();
                TempData["success"] = "Data berhasil diupdate.";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ======== Tambahan Fitur Delete ========
        public IActionResult Delete(int id)
        {
            var barang = _context.BarangMasuks.FirstOrDefault(x => x.Id == id);
            if (barang == null)
                return NotFound();

            _context.BarangMasuks.Remove(barang);
            _context.SaveChanges();

            TempData["success"] = "Data berhasil dihapus.";
            return RedirectToAction("Index");
        }
    }
}
