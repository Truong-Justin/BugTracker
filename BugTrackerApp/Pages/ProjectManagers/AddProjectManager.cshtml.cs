using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class AddProjectManagerModel : PageModel
    {
        public readonly IPeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }

        public AddProjectManagerModel(IPeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            // Insert a new project manager record
            // into the Project Managers table.
            try
            {
                _peopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, ProjectManager);
                return RedirectToPage("./ProjectManagerIndex");
            }

            // If an exception occurs, redirect
            // to the project manager index page.
            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./ProjectManagerIndex");
            }
        }
    }
}
