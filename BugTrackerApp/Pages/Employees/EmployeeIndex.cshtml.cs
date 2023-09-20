using BugTrackerApp.Models;
using BugTrackerApp.Models.People;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerApp.Pages.Employees
{
	public class EmployeeIndexModel : PageModel
    {
        public readonly PeopleDataAccess _peopleDataAccess;
        public required Employee Employee { get; set; }
        public required IList<Employee> Employees { get; set; }

        public EmployeeIndexModel(PeopleDataAccess peopleDataAccess)
        {
            _peopleDataAccess = peopleDataAccess;
        }

        public void OnGet()
        {
            Employees = _peopleDataAccess.GetAllPeople(Employee);
        }
    }
}
