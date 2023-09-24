using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages
{
	public class ProjectManagerIndexModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }

        public ProjectManagerIndexModel(IPeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet()
        {
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
        }
    }
}
