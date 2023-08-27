using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.ProjectManagers
{
	public class AddProjectManagerModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required ProjectManager ProjectManager { get; set; }

        public AddProjectManagerModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            try
            {
                _peopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, ProjectManager);
                return RedirectToPage("./ProjectManagerIndex");
            }

            catch (Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./ProjectManagerIndex");
            }
        }
    }
}
