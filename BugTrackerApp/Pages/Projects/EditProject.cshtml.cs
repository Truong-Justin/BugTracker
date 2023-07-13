using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTrackerApp.Pages.Projects
{
	public class EditProjectModel : PageModel
    {
        EntityDataAccess EntityDataAccess = new EntityDataAccess();
        public PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        [BindProperty]
        public required Project Project { get; set; }
        public required ProjectManager ProjectManager { get; set; }
        public required IList<ProjectManager> ProjectManagers { get; set; }
        [BindProperty]
        public int SelectedProjectManagerId { get; set; }


        public IActionResult OnGet(int id)
        {
            Project = EntityDataAccess.ViewEntity(id, Project);
            ProjectManagers = PeopleDataAccess.GetAllPeople(ProjectManager);
            return Page();
        }

        public IActionResult OnPost(int id, string projectTitle, string description, string priority)
        {
            try
            {
                projectTitle = Project.ProjectTitle;
                description = Project.Description;
                priority = Project.Priority;

                EntityDataAccess.EditEntity(id, projectTitle, description, priority, SelectedProjectManagerId, Project);
                return RedirectToPage("/Projects/ProjectIndex");
            }

            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                EntityDataAccess.EditEntity(Project.ProjectId, Project.ProjectTitle, Project.Description, Project.Priority, Project.ProjectManagerId, Project);
                return RedirectToPage("/Projects/ProjectIndex");
            }
        }
    }
}
