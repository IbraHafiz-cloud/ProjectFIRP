using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;

namespace ProjectFIRP.Controllers
{
    public class BarangKeluarController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BarangKeluar model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Simpan data ke database nanti
                TempData["success"] = "Barang keluar berhasil dicatat.";
                return RedirectToAction("Create");
            }

            return View(model);
        }
    }
}
