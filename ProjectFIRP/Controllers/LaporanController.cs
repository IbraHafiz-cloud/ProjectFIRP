using Microsoft.AspNetCore.Mvc;
using ProjectFIRP.Models;

namespace ProjectFIRP.Controllers
{
    public class LaporanController : Controller
    {
        public IActionResult Inventaris()
        {
            var laporan = new List<LaporanInventarisViewModel>
            {
                new LaporanInventarisViewModel { NamaBarang = "Paracetamol", TotalMasuk = 100, TotalKeluar = 40 },
                new LaporanInventarisViewModel { NamaBarang = "Amoxicillin", TotalMasuk = 80, TotalKeluar = 20 }
            };

            return View(laporan);
        }
    }
}
