using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;

namespace ProjectFIRP.Controllers
{
    public class StokObatController : Controller
    {
        public IActionResult Index()
        {
            // Simulasi data dummy
            var data = new List<StokObat>
            {
                new StokObat { Id = 1, NamaObat = "Paracetamol", Jumlah = 150, TanggalExpired = DateTime.Now.AddMonths(6), Keterangan = "Obat demam" },
                new StokObat { Id = 2, NamaObat = "Amoxicillin", Jumlah = 80, TanggalExpired = DateTime.Now.AddMonths(3), Keterangan = "Antibiotik" },
                new StokObat { Id = 3, NamaObat = "Vitamin C", Jumlah = 200, TanggalExpired = DateTime.Now.AddYears(1), Keterangan = "Suplemen" }
            };

            return View(data);
        }
    }
}
