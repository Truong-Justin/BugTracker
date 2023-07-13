using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;

namespace BugTrackerApp.Pages
{
	public class ViewBugModel : PageModel
    {
        EntityDataAccess ObjDataAccess = new EntityDataAccess();
        public required Bug Bug { get; set; }
        public required Project Project { get; set; }
             
        // Data from the selected bug is saved within the bug object
        // and used to output information to user on GET request.
        public void OnGet(int id)
        {
            Bug = ObjDataAccess.ViewEntity(id, Bug);
            Project = ObjDataAccess.ViewEntity(Bug.ProjectId, Project); 
        }

        public ActionResult OnPost(int id)
        {
            ObjDataAccess.DeleteEntity(id, Bug);
            return RedirectToPage("/Bugs/Index");
        }
    }
}
