using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;
using System.Collections.Generic;

namespace ProjectFIRP.Controllers
{
    public class TransaksiController : Controller
    {
        // Simulasi database sementara
        private static List<Transaksi> _transaksiList = new();

        public IActionResult Index()
        {
            return View(_transaksiList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaksi model)
        {
            if (ModelState.IsValid)
            {
                _transaksiList.Add(model);
                TempData["success"] = "Transaksi berhasil ditambahkan.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
