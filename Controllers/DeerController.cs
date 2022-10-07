using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc;

namespace databasConstruction.Controllers
{
    public class DeerController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.DeerNames = DeerModel.getAllDeer();
            return View();
        }
    }
}
