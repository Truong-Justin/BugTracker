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
        EntityDataAccess ObjDataAccess = new EntityDataAccess();
        public Bug Bug { get; set; }
             
        // Data from the selected bug is saved within the bug object
        // and used to output information to user on GET request.
        public void OnGet(int id)
        {
            Bug = ObjDataAccess.ViewEntity(id, Bug);
        }

        public ActionResult OnPost(int id)
        {
            ObjDataAccess.DeleteEntity(id, Bug);
            return RedirectToPage("/Index");
        }
    }
}
