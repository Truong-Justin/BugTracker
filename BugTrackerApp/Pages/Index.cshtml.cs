using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    BugDataAccess bugObj = new BugDataAccess();
    public List<Bug> bugs { get; set; }
    public int Id { get; set; }

    public void OnGet()
    {
        //Try to load page with list of bugs from bugs databaseb;
        //If database doesn't exist, create the database file
        try
        {
            bugs = bugObj.GetAllBugs();

            //If description is too long, truncate it so it doesn't
            //break the UI
            foreach (Bug bug in bugs.Where(b => b.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }

        }

        catch(System.Data.SQLite.SQLiteException)
        {
            bugObj.makeTable();
            bugs = bugObj.GetAllBugs();

            foreach (Bug bug in bugs.Where(b => b.Description.Length > 60))
            {
                bug.Description = bug.Description.Substring(0, 60) + "...";
            }
        }
    }

    //Delete a bug by ID on POST request
    public IActionResult OnPostDelete(int Id)
    {
        bugObj.deleteBug(Id);
        return RedirectToAction("Get");
    }

   
}

