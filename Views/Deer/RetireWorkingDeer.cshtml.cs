using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databasConstruction.Views.Deer
{
    public class RetireWorkingDeer : PageModel
    {
        public static DeerModel ChosenDeer { get; set; }
        public void OnGet(DeerModel deer)
        {
            ChosenDeer = deer;
        }
    }
}
