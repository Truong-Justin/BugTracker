using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class ProjectIndexModel : PageModel
    {
        public readonly EntityDataAccess _entityDataAccess;
        public required IList<Project> Projects { get; set; }
        Project Project { get; set; }

        public ProjectIndexModel(EntityDataAccess entityDataAccess)
        {
            _entityDataAccess = entityDataAccess;
        }

        public void OnGet()
        {
            // Try to load page with list of bugs from bugs database.
            try
            {
                Projects = _entityDataAccess.GetAllEntities(Project);
                Projects = _entityDataAccess.TruncateDescriptions(Projects);
            }

            // If database doesn't exist, create the database file and
            // set the journal mode to Write-Ahead-Logging
            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
            }
        }
    }
}
