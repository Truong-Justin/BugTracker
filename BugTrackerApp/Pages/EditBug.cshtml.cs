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


        //When the page is loaded, output a form to collect
        //new data from user to update bug record
        public IActionResult OnGet(int Id)
        {
            bug = objBug.viewBug(Id);
            return Page();
        }


        //Populate the fields with the attributes of the selected
        //bug the user wants to update; When the user enters the
        //new data and clicks the save button, the bug record is updated
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
