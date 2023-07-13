using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages
{
	public class ProjectIndexModel : PageModel
    {
        EntityDataAccess ObjDataAccess = new EntityDataAccess();
        public required IList<Project> Projects { get; set; }
        Project Project { get; set; }

        public void OnGet()
        {
            // Try to load page with list of bugs from bugs database.
            try
            {
                Projects = ObjDataAccess.GetAllEntities(Project);
                Projects = ObjDataAccess.TruncateDescriptions(Projects);
            }

            // If database doesn't exist, create the database file and
            // set the journal mode to Write-Ahead-Logging
            catch (Microsoft.Data.Sqlite.SqliteException)
            {
                ObjDataAccess.MakeTable();
                ObjDataAccess.SetJournalMode();
                Projects = ObjDataAccess.GetAllEntities(Project);
                Projects = ObjDataAccess.TruncateDescriptions(Projects);
            }
        }
    }
}
