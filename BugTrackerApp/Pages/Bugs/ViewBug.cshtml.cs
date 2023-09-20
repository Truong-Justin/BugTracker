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

             
        // A specific bug record is retrieved from the
        // Bugs table, and that bug record's project id
        // is used to retrieve the project the bug
        // belongs to.
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
