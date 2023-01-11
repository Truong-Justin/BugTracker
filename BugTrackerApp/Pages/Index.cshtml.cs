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
        bug = bugObj.GetAllBugs().ToList();
    }
}

