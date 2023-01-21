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

        [BindProperty]
        public Bug? bug { get; set; }

        public IActionResult OnGet(int Id)
        {
            bug = objBug.viewBug(Id);
            return Page();
        }

        public IActionResult OnPost(int Id, string Date, string Description, string Priority, string Assignment)
        {
            
            Date = bug.Date;
            Description = bug.Description;
            Priority = bug.Priority;
            Assignment = bug.Assignment;

            objBug.editBug(Id, Date, Description, Priority, Assignment);

            return RedirectToPage("Index");
        }
    }
}
