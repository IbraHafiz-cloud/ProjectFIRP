using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;

namespace ProjectFIRP.Controllers
{
    public class BarangMasukController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BarangMasuk model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Simpan data ke database
                TempData["success"] = "Barang masuk berhasil ditambahkan.";
                return RedirectToAction("Create");
            }

            return View(model);
        }
    }
}
