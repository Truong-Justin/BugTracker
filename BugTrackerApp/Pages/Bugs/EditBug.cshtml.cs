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
        EntityDataAccess ObjDataAccess = new EntityDataAccess();

        [BindProperty]
        public Bug Bug { get; set; }

        // When the page is loaded, output a form to collect
        // new data from user to update bug record.
        public IActionResult OnGet(int Id)
        {
            Bug = ObjDataAccess.ViewEntity(Id, Bug);
            return Page();
        }

        // Populate the fields with the attributes of the selected
        // bug the user wants to update.
        // When the user enters the
        // new data and clicks the save button, the bug record is updated.
        public IActionResult OnPost(int id, DateOnly date, string description, string priority, string assignment)
        {
            date = Bug.Date;
            description = Bug.Description;
            priority = Bug.Priority;
            assignment = Bug.Assignment;

            ObjDataAccess.EditEntity(id, date, description, priority, assignment, Bug);

            return RedirectToPage("Index");
        }
    }
}
