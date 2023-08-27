using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;

namespace BugTrackerApp.Pages
{
	public class ViewBugModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public required Bug Bug { get; set; }
        public required Project Project { get; set; }

        public ViewBugModel(EntityDataAccess entityDataAccess)
        {
            _entityDataAccess = entityDataAccess;
        }

             
        // Data from the selected bug is saved within the bug object
        // and used to output information to user on GET request.
        public void OnGet(int id)
        {
            Bug = _entityDataAccess.ViewEntity(id, Bug);
            Project = _entityDataAccess.ViewEntity(Bug.ProjectId, Project); 
        }

        public ActionResult OnPost(int id)
        {
            _entityDataAccess.DeleteEntity(id, Bug);
            return RedirectToPage("/Bugs/Index");
        }
    }
}
