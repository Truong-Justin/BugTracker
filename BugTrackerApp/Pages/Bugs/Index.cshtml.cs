using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages;

public class IndexModel : PageModel
{
    public readonly EntityDataAccess _entityDataAccess;
    public required IList<Bug> Bugs { get; set; }
    public required Bug Bug { get; set; }

    public IndexModel(EntityDataAccess entityDataAccess)
    {
        _entityDataAccess = entityDataAccess;
    }

    public void OnGet()
    {
        // Get all the bug records from the Bugs table,
        // and truncate the descriptions so that the cards
        // they populate in the View will all be uniform.
        try
        {
            Bugs = _entityDataAccess.GetAllEntities(Bug);
            Bugs = _entityDataAccess.TruncateDescriptions(Bugs);
        }

        // If an exception is thrown, output the exception
        // to console.
        catch (Exception exception)
        {
            Console.WriteLine("Error, exception: " + exception);
        }
    }   
}

