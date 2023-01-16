using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class EditBugModel : PageModel
    {
        BugDataAccess objBug = new BugDataAccess();

        public Bug bug { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string Date, string Description, string Priority, string Assignment)
        {
            if (Date is null)
                Date = bug.Date;

            if (Description is null)
                Description = bug.Description;

            if (Priority is null)
                Priority = bug.Priority;

            if (Assignment is null)
                Assignment = bug.Assignment;

            objBug.editBug(Date, Description, Priority, Assignment);
            return RedirectToPage("Index");
        }
    }
}
