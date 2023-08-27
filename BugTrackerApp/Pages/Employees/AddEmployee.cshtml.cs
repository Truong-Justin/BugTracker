using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class AddEmployeeModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required Employee Employee { get; set; }

        public AddEmployeeModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            try
            {
                _peopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, Employee);
                return RedirectToPage("./EmployeeIndex");
            }

            catch(Exception exception)
            {
                Console.WriteLine("Error, exception: " + exception);
                return RedirectToPage("./EmployeeIndex");
            }
        }
    }
}
