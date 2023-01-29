using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    BugDataAccess bugObj = new BugDataAccess();
    public List<Bug> bug { get; set; }

    public void OnGet()
    {
        //try to load page with list of bugs from bugs databaseb;
        //if database doesn't exist, create the database file
        try
        {
            bug = bugObj.GetAllBugs();
        }

        catch(System.Data.SQLite.SQLiteException)
        {
            bugObj.makeTable();
            bug = bugObj.GetAllBugs();
        }
    }

    //delete a bug by ID on POST request
    public IActionResult OnPostDelete(int Id)
    {
        bugObj.deleteBug(Id);
        return RedirectToAction("Get");
    }

   
}

