using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class AddEmployeeModel : PageModel
    {
        PeopleDataAccess PeopleDataAccess = new PeopleDataAccess();
        public required Employee Employee { get; set; }

        public ActionResult OnPost(string firstName, string lastName, DateOnly hireDate, string phone, string zip, string address)
        {
            try
            {
                PeopleDataAccess.AddPerson(firstName, lastName, hireDate, phone, zip, address, Employee);
                return RedirectToPage("./EmployeeIndex");
            }

            catch(Microsoft.Data.Sqlite.SqliteException)
            {
                return RedirectToPage("./EmployeeIndex");
            }
        }
    }
}
