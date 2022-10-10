using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace databasConstruction.Controllers
{
    public class DeerController : Controller
    {
        public IActionResult Index()
        {
            //if (id == null)
            //{
                ViewBag.DeerNames = DeerModel.GetAllDeers();
                return View();
            //}
            //try
            //{
            //    var list = DeerModel.GetAllDeers();
            //    Console.WriteLine("in selector function");
            //    foreach (var item in list)
            //    {
            //        if (item.DeerNr == id) item.Shown = true;
            //        else item.Shown = false;
            //    }
            //    ViewBag.DeerNames = list;
            //}
            //catch
            //{
            //    ViewBag.DeerNames = new List<DeerModel>();
            //    throw;
            //}

            return View();
        }


        public IActionResult RetireWorkingDeer(short deer)
        {
            Console.WriteLine(deer);
            ViewBag.WorkingDeer = DeerModel.GetById(deer);
            return View();
        }


        [HttpPost]
        public IActionResult Retire(int canNr, string factory, string taste, int id)
        {
            try
            {
                DeerModel.RetireDeerCall(canNr, factory, taste, id);
            }
            catch (Exception e)
            {
                throw new Exception("Error Werror uWu DX", e);
            }
            return View();
        }

    }
}