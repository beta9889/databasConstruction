using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace databasConstruction.Controllers
{
    public class DeerController : Controller
    {
        [HttpGet]
        public IActionResult Index(short? id)
        {
            if (id == null)
            {
                ViewBag.DeerNames = DeerModel.GetAllDeers();
                return View();
            }
            try
            {
                var list = DeerModel.GetAllDeers();
                Console.WriteLine("in selector function");
                foreach (var item in list)
                {
                    if (item.DeerNr == id) item.Shown = true;
                    else item.Shown = false;
                }
                ViewBag.DeerNames = list;
                ViewBag.DeerToDeer = null;
            }
            catch
            {
                ViewBag.DeerNames = new List<DeerModel>();
                throw;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(short id, short? selectedId)
        {
            if (selectedId != null)
                DeerToDeerModel.AddConnection(id, (short)selectedId);
            try
            {
                var deerList = DeerModel.GetAllDeers();

                foreach (var item in deerList)
                {
                    if (item.DeerNr == id)
                    {
                        item.Shown = true;
                        ViewBag.Selected = item;
                    }
                    else item.Shown = false;
                }
                ViewBag.DeerNames = deerList;
                var DeerToDeer = DeerToDeerModel.GetConnectionById(id);
                DeerToDeer.AddRange(DeerToDeerModel.GetConnectionById2(id));
                ViewBag.DeerToDeer = DeerToDeer;
            }
            catch
            {
                ViewBag.DeerNames = new List<DeerModel>();
                ViewBag.DeerToDeer = null;
                return Redirect(Url.Action("Error", "Home"));
            }

            return View();
        }

        public IActionResult RetireWorkingDeer(short deer)
        {
            try
            {
                ViewBag.WorkingDeer = DeerModel.GetById(deer);
            }
            catch
            {
                return Redirect(Url.Action("Error", "Home"));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Retire(int canNr, string factory, string taste, int id)
        {
            try
            {
                DeerModel.RetireDeerCall(canNr, factory, taste, id);
            }
            catch
            {

                return Redirect(Url.Action("Error", "Home"));
            }
            return View();
        }

    }
}