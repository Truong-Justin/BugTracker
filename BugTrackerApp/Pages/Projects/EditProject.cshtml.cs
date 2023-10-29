using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using BugTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages.Projects
{
	public class EditProjectModel : PageModel
    {
        public readonly IEntityDataAccess _entityDataAccess;
        public readonly IPeopleDataAccess _peopleDataAccess;
        [BindProperty]
        public required Project Project { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }


        public EditProjectModel(IEntityDataAccess entityDataAccess, IPeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        // Method retrieves the selected project record in order to
        // populate the form fields with the selected project's attributes.

        // Then a list of all project managers is retrieved from the database
        // and used to populate the select HTML element, so the user can choose
        // to assign a new project manager for the selected project.
        public IActionResult OnGet(int id)
        {
            Project = _entityDataAccess.ViewEntity(id, Project);
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }

        // Populates the form fields with the attributes of the selected
        // bug and performs a full or partial update. When the user enters
        // new data into the form, the bug record is updated with new values.
        public IActionResult OnPost(int id, string projectTitle, string description, string priority, int projectManagerId)
        {
            projectTitle = Project.ProjectTitle;
            description = Project.Description;
            priority = Project.Priority;
            projectManagerId = Project.ProjectManagerId;

            _entityDataAccess.EditEntity(id, projectTitle, description, priority, projectManagerId, Project);
            return RedirectToPage("./ProjectIndex");
        }
    }
}
