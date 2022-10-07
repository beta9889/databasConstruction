using Microsoft.AspNetCore.Mvc;

namespace databasConstruction.Controllers
{
    public class DeerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
