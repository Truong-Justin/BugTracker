using BugTrackerApp.Models.People;
using BugTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class ViewEmployeeModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required Employee Employee { get; set; }

        public ViewEmployeeModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet(int id)
        {
            Employee = _peopleDataAccess.ViewPerson(id, Employee);
        }

        public ActionResult OnPost(int id)
        {
            _peopleDataAccess.DeletePerson(id, Employee);
            return RedirectToPage("Employees/EmployeeIndex");
        }
    }
}
