using Microsoft.AspNetCore.Mvc;

namespace ProjectFIRP.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
