using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    EntityDataAccess ObjDataAccess = new EntityDataAccess();
    public required IList<Bug> Bugs { get; set; }
    Bug Bug { get; set; }

    public void OnGet()
    {
        // Try to load page with list of bugs from bugs database.
        try
        {
            Bugs = ObjDataAccess.GetAllEntities(Bug);
            Bugs = ObjDataAccess.TruncateDescriptions(Bugs);
        }

        // If database doesn't exist, create the database file and
        // set the journal mode to Write-Ahead-Logging
        catch (Microsoft.Data.Sqlite.SqliteException)
        {
            ObjDataAccess.MakeTable();
            ObjDataAccess.SetJournalMode();
            Bugs = ObjDataAccess.GetAllEntities(Bug);
            Bugs = ObjDataAccess.TruncateDescriptions(Bugs);
        }
    }   
}

