using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;

namespace BugTrackerApp.Pages
{
	public class ViewBugModel : PageModel
    {
        BugDataAccess objBug = new BugDataAccess();

        public Bug bug { get; set; }

        public ActionResult OnGet(int Id)
        {
            bug = objBug.viewBug(Id);

            return Page();
        }
    }
}
