using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages
{
	public class ProjectManagerIndexModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }

        public ProjectManagerIndexModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnGet()
        {
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }
    }
}
