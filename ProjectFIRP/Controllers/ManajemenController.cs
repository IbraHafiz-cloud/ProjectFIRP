using Microsoft.AspNetCore.Mvc;

namespace ProjectFIRP.Controllers
{
    public class ManajemenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
