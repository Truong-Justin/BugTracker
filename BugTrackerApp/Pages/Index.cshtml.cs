using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    EntityDataAccess ObjDataAccess = new EntityDataAccess();
    public IList<Bug> Bugs { get; set; }
    Bug Bug = new Bug();
    public int Id { get; set; }

    public void OnGet()
    {
        // Try to load page with list of bugs from bugs database.
        try
        {
            Bugs = ObjDataAccess.GetAllEntities(Bug);

            // If description is too long, truncate it so it doesn't
            // break the UI.
            foreach (Bug bug in Bugs.Where(bug => bug.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }
        }

        // If database doesn't exist, create the database file and
        // set the journal mode to Write-Ahead-Logging
        catch (System.Data.SQLite.SQLiteException)
        {
            ObjDataAccess.MakeTable();
            ObjDataAccess.SetJournalMode();
            Bugs = ObjDataAccess.GetAllEntities(Bug);

            foreach (Bug bug in Bugs.Where(bug => bug.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }
        }
    }   
}

