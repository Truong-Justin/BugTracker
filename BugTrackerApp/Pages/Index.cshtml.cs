using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    BugDataAccess BugObj = new BugDataAccess();
    public List<Bug> Bugs { get; set; }
    public int Id { get; set; }

    public void OnGet()
    {
        // Try to load page with list of bugs from bugs database.
        // If database doesn't exist, create the database file.
        try
        {
            Bugs = BugObj.GetAllBugs();

            // If description is too long, truncate it so it doesn't
            // break the UI.
            foreach (Bug bug in Bugs.Where(b => b.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }

        }

        catch(System.Data.SQLite.SQLiteException)
        {
            BugObj.MakeTable();
            Bugs = BugObj.GetAllBugs();

            foreach (Bug bug in Bugs.Where(b => b.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }
        }
    }

   
}

