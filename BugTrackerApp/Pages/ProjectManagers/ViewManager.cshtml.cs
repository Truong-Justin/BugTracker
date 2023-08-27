using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class ViewManagerModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }

        public ViewManagerModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet(int id)
        {
            ProjectManager = _peopleDataAccess.ViewPerson(id, ProjectManager);
        }

        public ActionResult OnPost(int id)
        {
            _peopleDataAccess.DeletePerson(id, ProjectManager);
            return RedirectToPage("/ProjectManagers/ProjectManagerIndex");
        }
    }
}
