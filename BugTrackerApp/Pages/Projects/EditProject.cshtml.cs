using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BugTrackerApp.Pages.Projects
{
	public class EditProjectModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public readonly PeopleDataAccess _peopleDataAccess;
        [BindProperty]
        public required Project Project { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }
        [BindProperty]
        public int SelectedProjectManagerId { get; set; }

        public EditProjectModel(EntityDataAccess entityDataAccess, PeopleDataAccess peopleDataAccess)
        {
            _entityDataAccess = entityDataAccess;
            _peopleDataAccess = peopleDataAccess;
        }


        public IActionResult OnGet(int id)
        {
            Project = _entityDataAccess.ViewEntity(id, Project);
            ProjectManagers = _peopleDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }

        public IActionResult OnPost(int id, string projectTitle, string description, string priority)
        {
            // Try to update the project record with user given values,
            // If an exception occurs default back to the original record values
            try
            {
                projectTitle = Project.ProjectTitle;
                description = Project.Description;
                priority = Project.Priority;
                SelectedProjectManagerId = Project.ProjectManagerId;

                _entityDataAccess.EditEntity(id, projectTitle, description, priority, SelectedProjectManagerId, Project);
                return RedirectToPage("/Projects/ProjectIndex");
            }

            catch (Exception exception)
            {
                _entityDataAccess.EditEntity(Project.ProjectId, Project.ProjectTitle, Project.Description, Project.Priority, Project.ProjectManagerId, Project);
                return RedirectToPage("/Projects/ProjectIndex");
            }
        }
    }
}
