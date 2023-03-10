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


        //data from the selected bug is saved within the bug object
        //and used to output information to user on GET request
        public void OnGet(int Id)
        {
            bug = objBug.viewBug(Id);
        }

        public ActionResult OnPost(int Id)
        {
            objBug.deleteBug(Id);
            return RedirectToPage("/Index");
        }
    }
}
