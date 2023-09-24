using BugTrackerApp.Models;
using BugTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class ProjectIndexModel : PageModel
    {
        public readonly IEntityDataAccess _entityDataAccess;
        public required IList<Project> Projects { get; set; }
        Project Project { get; set; }

        public ProjectIndexModel(IEntityDataAccess entityDataAccess)
        {
            _entityDataAccess = entityDataAccess;
        }

        public void OnGet()
        {
            // Get all the project records from the Projects table,
            // and truncate the descriptions and tiles so that the cards
            // they populate in the View will all be uniform
            try
            {
                Projects = _entityDataAccess.GetAllEntities(Project);
                Projects = _entityDataAccess.TruncateDescriptions(Projects);
                Projects = _entityDataAccess.TruncateTitles(Projects);
            }

            // If an exception is thrown, output it to the console
            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
            }
        }
    }
}
