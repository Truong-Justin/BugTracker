using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class ViewManagerModel : PageModel
    {
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required ProjectManager ProjectManager { get; set; }

        public void OnGet(int id)
        {
            ProjectManager = PeopleDataAccess.ViewPerson(id, ProjectManager);
        }

        public ActionResult OnPost(int id)
        {
            PeopleDataAccess.DeletePerson(id, ProjectManager);
            return RedirectToPage("/ProjectManagers/ProjectManagerIndex");
        }
    }
}
