using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc;

namespace databasConstruction.Controllers
{
    public class DeerController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.DeerNames = DeerModel.GetAllDeers();
            return View();
        }

        public IActionResult RetireWorkingDeer(short deer)
        {
            Console.WriteLine(deer);
            ViewBag.WorkingDeer = DeerModel.test(deer);
            return View();
        }

        [HttpPost]
        public IActionResult Retire(int canNr, string factory, string taste, int id)
        {
            try
            {
                DeerModel.RetireDeerCall(canNr, factory, taste, id);
            }
            catch(Exception e)
            {
                throw new Exception("Error Werror uWu DX", e);
            }
            return View();
        }

    }
}