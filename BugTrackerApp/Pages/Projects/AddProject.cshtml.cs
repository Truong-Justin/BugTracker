using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTrackerApp.Models;
using BugTrackerApp.Models.People;


namespace BugTrackerApp.Pages
{
    public class AddProjectModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        public Project Project { get; set; }
        public required IList<Project> Projects { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }
        [BindProperty]
        public int SelectedProjectManagerId { get; set; }

        public AddProjectModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnGet()
        {
            // Get a list of project managers and use their first name and
            // last name for project manager assignment
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
            
            return Page();
        }

        public ActionResult OnPost(DateOnly startDate, string projectTitle, string description, string priority)
        {
            // Insert a new project record into 
            // the Projects table.
            try
            {
                _entityDataAccess.AddEntity(SelectedProjectManagerId, startDate, projectTitle, description, priority, Project);
                return RedirectToPage("./ProjectIndex");
            }

            // If an exception occurs, redirect
            // back to the project index page.
            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./ProjectIndex");
            }
        }
    }
}
