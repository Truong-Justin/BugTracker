using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Projects
{
	public class ViewProjectModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        public required Project Project { get; set; }
        public ProjectManager ProjectManager { get; set; }

        public ViewProjectModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        public void OnGet(int id)
        {
            Project = _entityDataAccess.ViewEntity(id, Project);
            ProjectManager = _peopleDataAccess.ViewPerson(Project.ProjectManagerId, ProjectManager);
        }

        public ActionResult OnPost(int id)
        {
            _entityDataAccess.DeleteEntity(id, Project);
            return RedirectToPage("/Projects/ProjectIndex");
        }
    }
}
