using databasConstruction.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace databasConstruction.Views.Deer
{
    public class Index : PageModel
    {
        public void OnGet()
        {

        }
        public void OnPost()
        {

        }

         string getRetried(DeerModel deer)
        {
            return deer.Retired ? "Retired" : "Working";
        }

    }
}
